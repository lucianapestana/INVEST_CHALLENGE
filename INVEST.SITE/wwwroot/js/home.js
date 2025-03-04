document.addEventListener("DOMContentLoaded", async function () {
    getClient();
    getProducts();
});

var modalProduct = new bootstrap.Modal(document.getElementById('modal-select-product'));

const inputQtProduct = document.getElementById("qt-product");

inputQtProduct.addEventListener("input", function () {
    this.value = this.value.replace(/\D/g, "");

    const unitPrice = parseFloat(document.getElementById("unit-price-modal")?.getAttribute('data-unit-price')) || 0;
    const qt = parseFloat(inputQtProduct.value) || 0;
    const total = qt * unitPrice;

    const totalSpan = document.getElementById("total-value");
    totalSpan.innerText = total.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
    totalSpan.setAttribute("data-total", total);
});

inputQtProduct.addEventListener("paste", function (e) {
    let paste = (e.clipboardData || window.clipboardData).getData("text");
    if (!/^\d+$/.test(paste)) {
        e.preventDefault();
    }
});

document.getElementById("btn-close-modal").onclick = function () {
    cleanModalSelectProducts();
    modalProduct.hide();
}

function getClient() {
    const idClient = sessionStorage.getItem("idClient");

    $.ajax({
        url: `/json/client/${idClient}`,
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            const data = response.result;

            if (data != null && data.status == "success") {
                const dataClients = data.clients.length > 0 ? data.clients[0] : null;

                const client = document.getElementById("name-client");
                client.innerText = dataClients != null ? dataClients.name : "Usuário";
                client.setAttribute("data-account-client", dataClients != null ? btoa(dataClients.accountClient.accountClientId) : "0");

                const balanceClient = document.getElementById("balance-client");
                balanceClient.innerText = dataClients != null ? formatCurrency(dataClients.accountClient.balance) : "0,00";
                balanceClient.setAttribute("data-balance-client", dataClients != null ? dataClients.accountClient.balance : "0");
            }
        }
    });
}

function getProducts() {

    $.ajax({
        url: `/json/product/all`,
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            const data = response.result;

            if (data != null && data.status == "success") {

                const notProducts = document.getElementById("not-products-wrapper");
                const cardsProducts = document.getElementById("cards-products-wrapper");

                if (data.products.length > 0) {
                    createCardsProducts(data.products, cardsProducts);
                    cardsProducts.classList.remove('d-none');
                    notProducts.classList.add('d-none');
                }
                else {
                    cardsProducts.classList.add('d-none');
                    notProducts.classList.remove('d-none');
                }

            }
        }
    });
}

function createCardsProducts(products, cardsContainer) {
    cardsContainer.innerHTML = "";

    products.forEach(product => {

        const divWrapper = document.createElement("div");
        divWrapper.classList.add("col-sm-12", "col-md-6", "col-lg-4");
        cardsContainer.appendChild(divWrapper);

        /* Card */
        const divCardWrapper = document.createElement("div");
        divCardWrapper.classList.add("card", "h-100", "card-product");
        divWrapper.appendChild(divCardWrapper);

        /* Card Body */
        const divCardBodyWrapper = document.createElement("div");
        divCardBodyWrapper.classList.add("card-body", "d-flex", "flex-column");
        divCardWrapper.appendChild(divCardBodyWrapper);

        /* H5 */
        const divTitleProductWrapper = document.createElement("h5");
        divTitleProductWrapper.classList.add("text-center", "bond-asset");
        divTitleProductWrapper.setAttribute("title", product.bondAsset);
        divTitleProductWrapper.setAttribute("data-stock", product.stock);
        divCardBodyWrapper.appendChild(divTitleProductWrapper);

        /* Title */
        const titleCard = document.createTextNode(product.bondAsset);
        divTitleProductWrapper.appendChild(titleCard);

        /* Card Text Index */
        const cardText = document.createElement("p");
        cardText.classList.add("card-text");
        divCardBodyWrapper.appendChild(cardText);

        const indexText = document.createTextNode(`Indexador: ${product.index}`);
        cardText.appendChild(indexText);

        /* Card Text Unit Price */
        const cardTextUnitPrice = document.createElement("p");
        cardTextUnitPrice.classList.add("card-text", "unit-price");
        cardTextUnitPrice.setAttribute("data-unit-price", product.unitPrice);
        divCardBodyWrapper.appendChild(cardTextUnitPrice);

        const unitPriceFormated = formatCurrency(product.unitPrice);

        const unitPriceText = document.createTextNode(`Preço unitário: ${unitPriceFormated}`);
        cardTextUnitPrice.appendChild(unitPriceText);

        /* Card Text Tax */
        const cardTextTax = document.createElement("p");
        cardTextTax.classList.add("card-text");
        divCardBodyWrapper.appendChild(cardTextTax);

        const taxText = document.createTextNode(`Taxa: ${product.tax} %`);
        cardTextTax.appendChild(taxText);

        /* Button */
        const buttonSelect = document.createElement("a");
        buttonSelect.classList.add("btn", "btn-success", "w-100", "btn-buy");
        buttonSelect.setAttribute("id", `${btoa(product.productId)}`);
        divCardBodyWrapper.appendChild(buttonSelect);

        const textButton = document.createTextNode("COMPRAR");
        buttonSelect.appendChild(textButton);
    });
}

document.getElementById("cards-products-wrapper").addEventListener("click", (e) => {
    if (!e.target.classList.contains("btn-buy")) return;
    const id = atob(e.target.id);

    const card = e.target.closest(".card-product");
    if (!card) return;

    const unitPrice = card.querySelector(".unit-price")?.getAttribute('data-unit-price');
    const stock = card.querySelector(".bond-asset")?.getAttribute('data-stock');
    const bondAsset = card.querySelector(".bond-asset")?.innerText;

    selectProduct(id, unitPrice, stock, bondAsset);
});

function selectProduct(id, unitPrice, stock, bondAsset) {

    const bondAssetModal = document.getElementById("title-modal");
    bondAssetModal.innerText = bondAsset;
    bondAssetModal.setAttribute("data-stock-modal", stock);

    const balanceData = document.getElementById("balance-client")?.getAttribute("data-balance-client");
    const balanceModal = document.getElementById("balance-client-modal");
    balanceModal.innerText = formatCurrency(balanceData);

    const unitPriceModal = document.getElementById("unit-price-modal");
    unitPriceModal.innerText = formatCurrency(unitPrice);
    unitPriceModal.setAttribute("data-unit-price", unitPrice);

    const btnConfirm = document.getElementById("btn-confirm-buy");
    btnConfirm.setAttribute("data-i", btoa(id));

    modalProduct.show();
}

document.getElementById("btn-confirm-buy").onclick = function (e) {

    const id = e.target.getAttribute("data-i");
    const balance = document.getElementById("balance-client")?.getAttribute("data-balance-client");
    const stock = document.getElementById("title-modal").getAttribute("data-stock-modal");

    validateAvailableBalance(id, balance, stock);
}

function validateAvailableBalance(id, balance, stock) {

    const alertModal = document.getElementById("alert-modal");
    alertModal.classList.replace("d-block", "d-none");
    const messageAlertModal = document.getElementById("message-alert");
    messageAlertModal.innerText = "";

    if (!validateQtProduct()) { return; }

    const valueBalance = parseFloat(balance) || 0;
    const valueStock = parseFloat(stock) || 0;
    const qt = parseFloat(inputQtProduct.value) || 0;
    const total = parseFloat(document.getElementById("total-value")?.getAttribute('data-total')) || 0;

    if (qt > valueStock) {
        messageAlertModal.innerText = "A quantidade informada é maior do que a disponível em estoque.";
        alertModal.classList.replace("d-none", "d-block");
        return;
    }

    if (total > valueBalance) {
        messageAlertModal.innerText = "Saldo insuficiente! O total da compra é maior que o seu saldo disponível.";
        alertModal.classList.replace("d-none", "d-block");
        return;
    }

    orderClient(id, qt);
}

function validateQtProduct() {
    const spanQtProduct = document.getElementById("span-qt-product");

    if (inputQtProduct.value == "") {
        inputQtProduct.style.borderColor = "#dc3545"
        spanQtProduct.classList.replace("d-none", "d-block");
        return false;
    }
    else {
        inputQtProduct.style.borderColor = "#ced4da"
        spanQtProduct.classList.replace("d-block", "d-none");
        return true;
    }
}

function orderClient(idProduct, qt) {
    modalProduct.hide();

    const idAccount = document.getElementById("name-client")?.getAttribute("data-account-client");

    const payload = {
        accountClientId: atob(idAccount),
        productId: atob(idProduct),
        productQuantity: qt
    };

    $.ajax({
        url: "/json/order/client",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify(payload),
        success: function (response) {
            const data = response.result;

            if (data.status == "success") {
                openModalInfo("PARABÉNS", "Seu pedido foi processado com sucesso!", "text-success");
                return;
            }

            openModalInfo("ATENÇÃO", "Ocorreu um erro ao processar a compra. Tente novamente mais tarde.", "text-danger");
        },
        error: function () {
            openModalInfo("ATENÇÃO", "Falha ao realizar o pedido. Entre em contato com o suporte técnico.", "text-danger");
        }
    });
}

function cleanModalSelectProducts() {
    const alertModal = document.getElementById("alert-modal");
    alertModal.classList.replace("d-block", "d-none");
    const messageAlertModal = document.getElementById("message-alert");
    messageAlertModal.innerText = "";

    inputQtProduct.value = "";
    inputQtProduct.style.borderColor = "#ced4da"
    const spanQtProduct = document.getElementById("span-qt-product");
    spanQtProduct.classList.replace("d-block", "d-none");

    document.getElementById("total-value").innerText = "R$ 0,00";
}