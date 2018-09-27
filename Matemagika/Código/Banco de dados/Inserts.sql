--LEVEL
insert into level values(0, 'Explica��o Geral do Jogo')
insert into level values(1, 'Aritm�tica')
insert into level values(2, 'Fra��o')
insert into level values(3, '�lgebra')
insert into level values(4, 'Produto Not�vel')
insert into level values(5, 'Fatora��o')
insert into level values(6, 'Equa��o')

--QUIZ
insert into quiz values(1, 1, '2 + 7 =', '9', '5', '3', '2')
insert into quiz values(2, 1, '7:5 =', '1.4', '2.11', '7.33', '1.5')
insert into quiz values(3, 1, '0*5000 =', '0', '1', '5000', '2500')

insert into quiz values(1, 2, '2 + 9/2 =', '13/2', '11/2', '12/9', '15/7')
insert into quiz values(2, 2, '7/5 - 13/5 =', '-6/5', '20/5', '-4/5', '6/5')
insert into quiz values(3, 2, '2/3 + 3/4 =', '17/12', '7/7', '6/4', '14/12')

insert into quiz values(1, 3, '3x + 7x + 12 =', '10x + 12', '7x + 15', '11x + 12', '8x + 7')
insert into quiz values(2, 3, '12 + 4x + 1 +  17x =', '10x + 12', '7x + 15', '11x + 12', '8x + 7')
insert into quiz values(3, 3, 'x + 2x + 6 -3x + 7 =', '13', '0x + 11', '2x - 4', '8x + 7')
insert into quiz values(4, 3, 'x + 2x� + 6 -3x� + x + 7 =', '-3x� + 2x� + 2x + 13', '3x� + 4x + 11', '7x� + 13', '7x� + 13')

insert into quiz values(1, 4, '3(x + 2y) =', '6y + 3x', '6x + 3x', '2x + 4y', '3y + x')
insert into quiz values(2, 4, '(x - 7)� =', 'x� -14x + 49', '7x + 15', '11x + 12', '8x + 7')
insert into quiz values(3, 4, '(3x + 8y�)� =', '9x� + 48xy� + 64y^4', '3x� + 24xy� + 8y^4', '3x� + 48xy� + 8y^4', '24xy�')

insert into quiz values(1, 5, '6y + 3x =', '3(x + 2y)', '6(x + y/3)', '2(3x + 7y)', 'N�o h� o que fatorar.')
insert into quiz values(2, 5, 'x� -14x + 49 =', '(x - 7)�', '7x + 15', '3(x� - 7)', '(x + 7)(x - 7)')
insert into quiz values(3, 5, 'x� + 10xy� + 25y^4 =', '(x + 5y�)�', '(x + 3y�)�', '(2x + 10y�)�', '(2x + 10y�)(2x - 10y�)')

insert into quiz values(1, 6, '3x + 12 = 0', 'x= -4', 'x= 2', 'x= 14/3', 'x= 0')
insert into quiz values(2, 6, 'x + 7 = 2x + 8', 'x= -1', 'x= 1', 'x = 13/2', 'x= 0')
insert into quiz values(3, 6, '3x - x = 7x + 11', 'x= 11/5', 'x= 11/7', 'x= 13/9', 'x= -11/3')

select * from quiz order by level, codQuiz

--EXPLICACAO (e contextualizacao)
insert into explicacao values(0, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(0, 1, 'ESSA EH A contextualizacao 1', 0)
insert into explicacao values(1, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(1, 1, 'ESSA EH A contextualizacao 1', 0)
insert into explicacao values(2, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(2, 1, 'ESSA EH A contextualizacao 1', 0)
insert into explicacao values(3, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(3, 1, 'ESSA EH A contextualizacao 1', 0)
insert into explicacao values(4, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(4, 1, 'ESSA EH A contextualizacao 1', 0)
insert into explicacao values(5, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(5, 1, 'ESSA EH A contextualizacao 1', 0)
insert into explicacao values(6, 1, 'ESSA EH A EXPLICACAO 1', 1)
insert into explicacao values(6, 1, 'ESSA EH A contextualizacao 1', 0)


sp_help explicacao
select * from explicacao

--inimigo
--personagem
(ARRUMAR "PEGAR PERSONAGENS")

select * from usuario