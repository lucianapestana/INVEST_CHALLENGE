USE [INVEST]
GO

INSERT INTO TB_CLIENTS (NAME, USERNAME, PASSWORD)
SELECT 'Administrador', 'adm', 'QWRtMTIz' --Adm123
GO

INSERT INTO TB_ACCOUNTS_CLIENTS (ACCOUNT, CLIENT_ID, BALANCE)
SELECT '00001', 1, 1000.00
GO

INSERT INTO TB_PRODUCTS (BOND_ASSET, INDEXADOR, TAX, ISSUER_NAME, UNIT_PRICE, STOCK)
SELECT 'CDB', 'IPCA', 5.0, 'Banco Teste', 1000, 100 UNION ALL
SELECT 'LCI', 'Pre', 12.0, 'Banco Teste 2', 200, 20 UNION ALL
SELECT 'TESOURO DIRETO', 'Selic', 7.0, 'Banco Teste 3', 400, 50 UNION ALL
SELECT 'RCI', 'IPCA', 6.0, 'Banco Teste 4', 100, 80 UNION ALL
SELECT 'FUNDOS RENDA FIXA', 'Selic', 15.0, 'Banco Teste 5', 900, 10 UNION ALL
SELECT 'RCA ', 'CDI', 9.0, 'Banco Teste 6', 600, 2
GO
