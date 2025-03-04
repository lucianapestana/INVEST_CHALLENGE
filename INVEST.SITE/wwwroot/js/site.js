const formatCurrency = (value) => {
    return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
        minimumFractionDigits: 2
    }).format(value || 0);
};

var modalInfo = new bootstrap.Modal(document.getElementById('modal-info'));

function openModalInfo(title, message, classModal) {
    
    const titleModalInfo = document.getElementById("title-modal-info");
    titleModalInfo.classList.remove();
    titleModalInfo.classList.add("modal-title", classModal);
    titleModalInfo.innerText = title;

    document.getElementById("text-modal-info").innerText = message;

    modalInfo.show();
}

document.getElementById("btn-close-modal-info").onclick = function () {
    modalInfo.hide();
    window.location.reload();
}