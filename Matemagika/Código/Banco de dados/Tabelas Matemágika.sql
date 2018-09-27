create table level(
codLv int primary key, --level 0=explicacao do jogo
nome  varchar(50) not null
)

create table explicacao(
codContext       int identity(1,1) primary key,
codLv            int not null,
nPasso           int not null,
texto            varchar(200) not null,
explicacao bit not null, --1: explicacao, 0: contextualizacao

constraint fkLvExp2 foreign key(codLv)
references level(codLv)
)

create table usuario(
nome   varchar(20) primary key,
senha  varchar(64) not null,
xp     int not null,
moedas int not null,

constraint chkXp check (xp>=0),
constraint chkMoeda check (moedas>=0)
)

create table levelUs(
codLvUs  int identity(1,1) primary key,
nomeUs   varchar(20) not null,
codLv    int not null,
estrelas int not null, -- se 3 estrelas de ouro: estrelas=4

constraint fkNomeUs foreign key(nomeUs)
references usuario(nome),
constraint fkLvUs foreign key(codLv)
references level(codLv)
)

create table quiz(
codQuiz   int not null,
level     int not null,
pergunta  varchar(100) not null,
opcao1    varchar(25) not null,
opcao2    varchar(25) not null,
opcao3    varchar(25) not null,
opcao4    varchar(25) not null,
-- opCorreta int not null -> podemos fazer todas as opcoes certas na primeira e no programa fazemos um random()

constraint pk primary key(codQuiz, level),
constraint fkLv foreign key(level) references level(codLv)
)

create table personagem(
codPers int primary key identity,
nome varchar(40) not null,
imagem0 Image not null, -- morto direita
imagem1 Image not null, -- direita
imagem2 Image not null, -- esquerda
imagem3 Image not null, -- morto esquerda
preco int not null
)

create table personagemComprado(
codPersComprado int identity(1,1) primary key,
nomeUs varchar(20) not null,
codPers int not null,

constraint fkPers foreign key(codPers) references personagem(codPers)
)

create table usDisponJogarContra(
porta int,
ip varchar(20),
nome varchar(20) not null,
online bit not null, --0:offline, 1:online

constraint fkNomeDispon foreign key(nome) references usuario(nome),
constraint pkPortaIp primary key(porta, ip)
)