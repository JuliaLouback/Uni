use Uni

INSERT INTO NCM VALUES 
('25232910','Cal,Cimento Comum'),
('25051000','Areia'),
('1002700','Tijolos para construção, tijoleiras, tapa-vigas e produtos semelhantes, de cerâmica'),
('68022900','Obras de pedra, gesso, cimento, amianto, mica ou de matérias semelhantes '),
('82159990','Ferramentas, artefatos de cutelaria e talheres, e suas partes, de metais comuns'),
('87168000','Veículos automóveis, tratores, ciclos e outros veículos terrestres, suas partes e acessórios -'),
('90269010','De instrumentos e aparelhos para medida ou controle do nível'),
('90178010','Instrumentos e aparelhos de óptica, fotografia ou cinematografia, medida, controle ou de precisão'),
('96034010','Obras diversas'),
('69089000','Produtos cerâmicos - Ladrilhos e placas (lajes), para pavimentação ou revestimento, vidrados ou esmaltados, de cerâmica'),
('82052000','Martelos e marretas'),
('73170090','Obras de ferro fundido, ferro ou aço'),
('85441100','Fios de Cobre'),
('85441910','Fios de Alumínio'),
('85393100','Máquinas, aparelhos e materiais elétricos, e suas partes'),
('84818019','Torneiras, válvulas (incluídas as redutoras de pressão e as termostáticas) e dispositivos semelhantes, para canalizações, caldeiras, reservatórios, cubas e outros recipientes')

delete from NCM

insert into CFOP values ('5102',' Venda de mercadoria adquirida ou recebida de terceiros'),
('5403','Venda de mercadoria, adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária, na condição de **contribuinte-substituto**'),
('5405','Venda de mercadoria, adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária, na condição de contribuinte-substituído'),
('5115','Venda de mercadoria adquirida ou recebida de terceiros, **RECEBIDA** anteriormente em consignação mercantil'),
('5117','Venda de mercadoria adquirida ou recebida de terceiros, originada de encomenda para entrega futura')

insert into CST values ('000','Nacional, exceto as indicadas nos códigos 3 a 5;  Tributada integralmente'),
('010','Nacional, exceto as indicadas nos códigos 3 a 5; Tributada e com cobrança do ICMS por substituição tributária'),
('020','Nacional, exceto as indicadas nos códigos 3 a 5; Com redução de base de cálculo'),
('030','Nacional, exceto as indicadas nos códigos 3 a 5; Isenta ou não tributada e com cobrança do ICMS por substituição tributária'),
('040','Nacional, exceto as indicadas nos códigos 3 a 5; Isenta'),
('041','Nacional, exceto as indicadas nos códigos 3 a 5; Não tributada'),
('050','Nacional, exceto as indicadas nos códigos 3 a 5; Suspensão'),
('051','Nacional, exceto as indicadas nos códigos 3 a 5; Diferimento'),
('060','Nacional, exceto as indicadas nos códigos 3 a 5; ICMS cobrado anteriormente por substituição tributária'),
('070','Nacional, exceto as indicadas nos códigos 3 a 5; Com redução de base de cálculo e cobrança do ICMS por substituição tributária'),
('080','Nacional, exceto as indicadas nos códigos 3 a 5; Outras'),

('100','Estrangeira - Importação direta, exceto a indicada no código 6;  Tributada integralmente'),
('110','Estrangeira - Importação direta, exceto a indicada no código 6; Tributada e com cobrança do ICMS por substituição tributária'),

('220','Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7; Com redução de base de cálculo'),
('230','Estrangeira - Adquirida no mercado interno, exceto a indicada no código 7;Isenta ou não tributada e com cobrança do ICMS por substituição tributária'),

('340','Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40%; Isenta'),
('341','Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40%; Não tributada'),

('450',' Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei nº 288/1967 , e as Leis nºs 8.248/1991, 8.387/1991, 10.176/2001 e 11.484/2007; Suspensão'),
('451',' Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei nº 288/1967 , e as Leis nºs 8.248/1991, 8.387/1991, 10.176/2001 e 11.484/2007; Diferimento'),

('560', 'Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40%; ICMS cobrado anteriormente por substituição tributária'),
('570',' Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40%; Com redução de base de cálculo e cobrança do ICMS por substituição tributária'),

('680','Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução Camex e gás natural; Outras')