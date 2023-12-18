PONTIFÍCIA UNIVERSIDADE CATÓLICA DE MINAS GERAIS
Algoritmos e Técnicas de Programação – 02/2023
Instruções:
I. O trabalho deverá ser feito individualmente.
II. O trabalho deverá ser realizado usando a linguagem de programação C#.
III. Deverão usar os conceitos aprendidos na disciplina Algoritmos e Estruturas de
Dados, levando em consideração as melhores estruturas para representar os itens
do jogo.
IV. O trabalho deverá ser entregue até a data 03/12/2023, via Canvas.
V. Somente poderão ser utilizadas estruturas e conceitos vistos em sala de aula.
VI. A avaliação do trabalho será por meio de apresentação do código.
VII. Comece a fazer este trabalho logo, enquanto o problema está fresco na memória e
o prazo para terminá-lo está tão longe quanto jamais poderá estar;
1 – Descrição do trabalho
Desenvolver uma aplicação que simule o jogo Bingo, a seguir serão apresentadas as regras
e instruções do jogo as quais devem ser seguidas no desenvolvimento do trabalho prático.
2 - Instruções
a) Cada jogador pode usar de 1 a 4 cartelas de 24 números aleatórios de 1 a 75;
b) A cada rodada um número é sorteado e o jogador verifica se ele está na sua cartela;
c) O jogador completa sua(s) cartela(s) marcando os números sorteados;
d) O objetivo é completar linhas ou colunas;
A seguir a Figura 1, apresenta uma imagem ilustrativa de uma cartela do jogo de Bingo.
3 - Regras
Existem várias variações e diferentes regras para esse jogo. Para manter a uniformidade
de implementação, serão dispostas as regras que a implementação deve atender.
Variações não serão permitidas.
1. O jogo de Bingo pode permitir que vários jogadores joguem uma mesma “partida”.
Para padronizar, a aplicação de vocês deverá permitir que se escolha quantos
jogadores disputarão uma determinada partida, sendo o número mínimo de
jogadores iguais a 2 e sendo o número máximo igual a 5 jogadores.
2. Cada jogador poderá escolher com quantas cartelas, este irá jogar. Sendo,
evidentemente, que o número mínimo será igual a 1 cartela e o número máximo igual
a 4 cartelas.
3. Tendo estas definições, o jogo poderá se iniciar, de modo que o seu programa
deverá sortear um número entre 1 e 75. Após o sorteio cada jogador irá conferir em
sua(s) cartela(s) e caso o número sorteado esteja em sua cartela, este deverá ser
marcado. Ressalta-se que os números sorteados não se repetem, sendo assim, uma
vez sorteado um número este não voltará a ser sorteado;
4. De acordo com as regras, um jogador deverá cantar Bingo (manifestar que
conseguiu completar um dos padrões para completar a cartela). Ressaltando que os
padrões para se poder cantar Bingo encontra-se neste documento na instrução d.
Salienta-se que se o jogador estiver jogando com mais de uma cartela, basta
completar os padrões em ao menos uma das cartelas;
5. Após um jogador cantar Bingo, seu programa deverá verificar se de fato este
conseguiu completar os padrões. Ex: Suponha que o jogador disse ter completado o
padrão o qual preencheu toda a 3ª linha da cartela, sendo assim, seu programa
deverá verificar se todos os valores presentes na 3ª linha da cartela de fato foram
sorteados. Se sim, este jogador de fato encerrou sua partida, senão, esta cartela é
anulada;
6. Se o jogador estiver jogando apenas com uma cartela e cantar Bingo, de forma
equivocada, este automaticamente perderá a partida, e sairá da disputa, estando na
última posição do ranking;
7. O jogo termina quando o penúltimo jogador cantar Bingo. Ex: Suponha um jogo que
tenha 3 jogadores. Um dos jogadores completa um dos padrões, em seguida o jogo
continuará do mesmo ponto para os dois jogadores restantes, até que um destes
cante Bingo. Sendo validado, aí sim a partida se encerrará;
8. Após encerrada a partida, seu programa deverá apresentar o ranking da partida, que
conterá as informações dos jogadores;
9. As informações dos jogadores a serem apresentadas são: nome do jogador, idade e
sexo. Obs: deverão criar uma classe jogador para conseguir armazenar as
informações;
4 – Definições da cartela
• Cada cartela é composta por 24 números aleatórios de 1 a 75, dispostos em uma
grade de 5 linhas e 5 colunas. O espaço central da cartela, é vazio, conforme se
verifica na Figura 1;
• Todas as cartelas devem obrigatoriamente serem únicas, ou seja, os valores
deverão ser diferentes para cada uma das cartelas que farão parte do jogo. Seu
programa deverá validar isso;
• Apesar de os números serem gerados aleatoriamente, a disposição deles segue uma
ordem, para facilitar a compreensão e a marcação dos números pelo jogador,
conforme tabela abaixo:
Coluna B -> 1 a 15
Coluna I -> 16 a 30
Coluna N -> 31 a 45
Coluna G -> 46 a 60
Coluna O -> 61 a 75
5 - Requisitos
1. Utilização de uma classe (para representar os dados do jogador).
2. Comandos de repetição.
3. Utilização de Funções/métodos.
4. Utilização de Matriz(es) e/ou Vetor(es).
5. Nomes intuitivos para Classes, Funções/métodos e Variáveis.
6. Permitir que o usuário escolha a quantidade de jogadores (2 a 5).
7. Código indentado e comentado.
6 - Avaliação
O trabalho deve ser feito individualmente. O trabalho deverá ser apresentado ao professor
da disciplina no final do semestre. Trabalhos copiados, parcialmente ou integralmente,
serão avaliados com nota zero, sem direito a contestação. Ressaltando que deverão usar
apenas instruções/comandos vistos em sala de aula.
