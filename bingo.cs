using System;
using System.Data;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            int numeroJogadores;
            do
            {
                Console.WriteLine("\nBem vindo ao Bingo! Digite a quantidade de jogadores que irão jogar (2 a 5)");
                numeroJogadores = int.Parse(Console.ReadLine());

                if (numeroJogadores > 5 || numeroJogadores < 2)
                {
                    Console.WriteLine("\nNúmero de jogadoes incompatível com as regras");
                }
            } while (numeroJogadores > 5 || numeroJogadores < 2);

            Players jogadores = new Players(numeroJogadores);

            for (int i = 0; i < numeroJogadores; i++)
            {
                Console.WriteLine($"\nDigite os dados do jogador {i + 1}:");

                Console.Write("Nome: ");
                string nome = Console.ReadLine();

                int idade;
                do
                {
                    Console.Write("Idade: ");
                    idade = int.Parse(Console.ReadLine());
                    if (idade <= 0)
                    {
                        Console.WriteLine("\nNasceu agora doido? Digita uma idade válida amigo.");
                    }
                } while (idade <= 0);

                Console.Write("Gênero: ");
                string sexo = (Console.ReadLine().ToUpper());

                int quantCartelas;
                do
                {
                    Console.Write("Quantidade de Cartelas (1 a 4): ");
                    quantCartelas = int.Parse(Console.ReadLine());

                    if (quantCartelas < 1 || quantCartelas > 4)
                    {
                        Console.WriteLine("\nQuantidade inválida de cartelas, digite uma quantidade válida");
                    }

                } while (quantCartelas < 1 || quantCartelas > 4);

                Jogadores jogador = new Jogadores(nome, idade, sexo, quantCartelas);
                jogadores.AdicionarJogador(jogador);
            }

            Console.WriteLine("\nVamos começar o jogo? (Sim ou Nao)");
            string comeco = Console.ReadLine().ToUpper();
            if (comeco == "SIM")
            {
                Console.Clear();
                Gameplay(jogadores, numeroJogadores);
            }
            else
            {
                Console.WriteLine("\nVAI COMEÇAR SIM!!");
                Console.ReadLine();
                Console.Clear();
                Gameplay(jogadores, numeroJogadores);
            }
            Console.ReadLine();
        }

        //aqui que o jogo acontece
        static void Gameplay(Players jogadores, int numeroJogadores)
        {
            string[] ranking = new string[numeroJogadores];
            int Jganhou = 0;
            int Jperdeu = ranking.Length - 1;
            Roleta roleta = new Roleta();
            int bingo = 0;
            bool gameplayRolando = true;

            while (gameplayRolando)
            {
                int numeroSorteado = roleta.GirarRoleta();

                for (int i = 0; i < jogadores.Jogando.Length; i++)
                {
                    Jogadores jogador = jogadores.Jogando[i]; ;

                    // Verifica se o jogador está jogando
                    if (jogador.Jogando)
                    {
                        Console.WriteLine("\nO número sorteado é:");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(numeroSorteado);
                        Console.ResetColor();
                        ImprimirCopiasCartelas(jogador);
                        string resposta;
                        do
                        {
                            Console.WriteLine($"\n{jogador.Nome}, você possui o número sorteado em alguma das suas carterlas? (sim ou nao)");
                            resposta = Console.ReadLine().ToUpper();

                            if (resposta != "SIM" && resposta != "NAO")
                            {
                                Console.WriteLine("\nAmigão, colabora comigo e escreve o que eu pedi?");
                            }

                        } while (resposta != "SIM" && resposta != "NAO");

                        if (resposta == "SIM")
                        {
                            Console.WriteLine("\nEm quantas cartelas o número está presente?");
                            int numeroCartelas = int.Parse(Console.ReadLine());

                            for (int j = 0; j < numeroCartelas; j++)
                            {
                                Console.WriteLine($"\nInforme a {j + 1}° cartela em que o número {numeroSorteado} está presente:");
                                int posicaoCartela = int.Parse(Console.ReadLine());

                                if (posicaoCartela >= 1 && posicaoCartela <= jogador.cartelasCopias.Length)
                                {
                                    MarcarNumeroCartela(jogador, numeroSorteado, posicaoCartela);
                                    ImprimirCopiasCartelas(jogador);
                                    Console.WriteLine($"\nCartelas de {jogador.Nome} foram atualizadas.");
                                }
                                else
                                {
                                    Console.WriteLine("Posição inválida. Tente novamente.");
                                    j--; // Decrementa o índice para tentar novamente na mesma iteração
                                }
                            }
                        }

                        //verificação de bingo
                        Console.WriteLine($"{jogador.Nome}, você fez bingo em alguma cartela? (SIM OU NAO)");
                        string binguei = Console.ReadLine().ToUpper();

                        if (binguei == "SIM")
                        {
                            ImprimirCopiasCartelas(jogador);
                            Console.WriteLine("\nEm qual cartela?");
                            int cartelaBinguei = int.Parse(Console.ReadLine());
                            string linha_coluna;
                            do
                            {
                                Console.WriteLine($"\nFoi na linha ou coluna da cartela {cartelaBinguei}");
                                linha_coluna = Console.ReadLine().ToUpper();

                                if (linha_coluna != "LINHA" && linha_coluna != "COLUNA")
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nPor favor amigo, digite certo pelo menos uma vez");
                                    Console.ResetColor();
                                }

                            } while (linha_coluna != "LINHA" && linha_coluna != "COLUNA");

                            if (linha_coluna == "LINHA")
                            {
                                Console.WriteLine("\nQual linha?");
                                int linha = int.Parse(Console.ReadLine());
                                bool ganhou = VerificarBingoLinha(jogador, cartelaBinguei, linha);

                                if (ganhou)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nÉ isso ai paizão, você fez bingo!!!");
                                    Console.ResetColor();
                                    ranking[Jganhou] = jogador.Nome;
                                    Jganhou++;
                                }
                                else if (!ganhou)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nÉ paizão, parece que você errou.");
                                    Console.ResetColor();
                                    if (jogador.cartelasCopias.Length > 1)
                                    {
                                        jogador.CartelaErrada(cartelaBinguei);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"\nA cartela {cartelaBinguei} foi removida");
                                        Console.ResetColor();
                                        ImprimirCopiasCartelas(jogador);
                                    }
                                    else //jogador vai sair do jogo
                                    {
                                        ranking[Jperdeu] = jogador.Nome;
                                        Jperdeu--;
                                        jogador.Jogando = false;
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nVocê não possui mais cartelas e será removido do jogo.");
                                        Console.ResetColor();
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nQual coluna?");
                                int coluna = int.Parse(Console.ReadLine());
                                bool ganhou1 = VerificarBingoColuna(jogador, cartelaBinguei, coluna);

                                if (ganhou1)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nÉ isso ai paizão, você fez bingo!!!");
                                    Console.ResetColor();
                                    ranking[Jganhou] = jogador.Nome;
                                    Jganhou++;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nÉ paizão, parece que você errou.");
                                    Console.ResetColor();
                                    if (jogador.cartelasCopias.Length > 1)
                                    {
                                        jogador.CartelaErrada(cartelaBinguei);
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"\nA cartela {cartelaBinguei} foi removida");
                                        Console.ResetColor();
                                        ImprimirCopiasCartelas(jogador);
                                    }
                                    else //jogador vai sair do jogo
                                    {
                                        ranking[Jperdeu] = jogador.Nome;
                                        Jperdeu--;
                                        jogador.Jogando = false;
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nVocê não possui mais cartelas e será removido do jogo.");
                                        Console.ResetColor();
                                    }
                                }
                            }
                        }
                    }
                    Console.ReadLine();
                    Console.Clear();
                }

                int jogadoresAtivos = 0;
                int posiUltimoJogador = 0;
                for (int i = 0; i < jogadores.Jogando.Length; i++)
                {
                    if (jogadores.Jogando[i].Jogando)
                    {
                        jogadoresAtivos++;
                        posiUltimoJogador = i;
                    }
                }

                if (jogadoresAtivos == 1)
                {
                    Console.WriteLine("\nResta apenas um jogador, portanto, o jogo será encerrado");
                    gameplayRolando = false;
                    for (int i = 0; i < ranking.Length; i++)
                    {
                        if (ranking[i] == null)
                        {
                            ranking[i] = jogadores.Jogando[posiUltimoJogador].Nome;
                        }
                    }
                    Console.WriteLine("\nO ranking com a posição dos jogadores é:");
                    for (int i = 0; i < ranking.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"{i + 1} - " + $"{ranking[i]}");
                    }
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nMEUS PARABÉNS JOGADOR {ranking[0]}!!!! Você é o campeão do bingo!");
                    Console.ResetColor();

                    Console.WriteLine("\nAqui estão os dados de todos os jogadores que participaram:");
                    for (int i = 0; i < ranking.Length; i++)
                    {
                        Console.WriteLine($"\nJogador{i + 1}");
                        Console.WriteLine($"Nome: {jogadores.Jogando[i].Nome}");
                        Console.WriteLine($"Idade: {jogadores.Jogando[i].Idade}");
                        Console.WriteLine($"Sexo: {jogadores.Jogando[i].Sexo}");
                    }
                }
            }
        }

        //verificar bingo
        static bool VerificarBingoLinha(Jogadores jogador, int cartelaNumero, int linha)
        {
            Cartela cartelaOriginal = jogador.cartelasOriginais[cartelaNumero - 1];
            Roleta verificacao = new Roleta();
            int[] final = verificacao.VetorSorteados();

            int[,] matrizCartela = cartelaOriginal.ObterCartela();
            int cont = 0;

            bool binguei = false;
            //verificar o bingo nas linhas
            for (int i = linha; i < linha + 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    foreach (int sorteado in final)
                    {
                        if (sorteado == matrizCartela[i, j])
                        {
                            cont++;
                            break;
                        }
                    }
                }
            }

            if (linha == 2)
            {
                if (cont == 4)
                {
                    binguei = true;
                }
            }
            else if (linha != 2)
            {
                if (cont == 5)
                {
                    binguei = true;
                }
            }
            return binguei;

        }
        static bool VerificarBingoColuna(Jogadores jogador, int cartelaNumero, int coluna)
        {
            Cartela cartelaOriginal = jogador.cartelasOriginais[cartelaNumero - 1];
            Roleta verificacao = new Roleta();
            int[] final = verificacao.VetorSorteados();

            int[,] matrizCartela = cartelaOriginal.ObterCartela();
            int cont = 0;

            bool binguei = false;
            //verificar o bingo nas linhas
            for (int i = coluna; i < coluna + 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    foreach (int sorteado in final)
                    {
                        if (sorteado == matrizCartela[j, i])
                        {
                            cont++;
                            break;
                        }
                    }
                }
            }

            if (coluna == 2)
            {
                if (cont == 4)
                {
                    binguei = true;
                }
            }
            else if (coluna != 2)
            {
                if (cont == 5)
                {
                    binguei = true;
                }
            }
            return binguei;

        }

        //marca os números na cartela
        static void MarcarNumeroCartela(Jogadores jogador, int numeroSorteado, int posicaoCartela)
        {
            Console.WriteLine($"\nDeseja marcar o número {numeroSorteado} na Cartela {posicaoCartela}? (sim ou nao)");
            string resposta = Console.ReadLine().ToUpper();

            if (resposta == "SIM")
            {
                // Marque o número na cartela cópia
                bool marcado = MarcarNumero(jogador.cartelasCopias[posicaoCartela - 1], numeroSorteado);

                if (marcado)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Número {numeroSorteado} marcado na Cartela {posicaoCartela} de {jogador.Nome}.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Número {numeroSorteado} não existe na Cartela {posicaoCartela} de {jogador.Nome}.");
                    Console.ResetColor();
                }
            }
        }

        //substitui o número marcado por 0
        static bool MarcarNumero(Cartela cartela, int numero)
        {
            int[,] matrizCartela = cartela.ObterCartela();
            bool certo = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrizCartela[i, j] == numero)
                    {
                        // Marque o número na cartela
                        matrizCartela[i, j] = 0;
                        certo = true;
                    }
                }
            }
            return certo;
        }

        //imprimir cópias das cartelas na tela
        static void ImprimirCopiasCartelas(Jogadores jogador)
        {
            Console.WriteLine($"\nCartelas do Jogador:{jogador.Nome}");

            for (int i = 0; i < jogador.cartelasCopias.Length; i++)
            {
                Console.WriteLine($"\nCartela {i + 1}:");
                ImprimirCartelas(jogador.cartelasCopias[i].ObterCartela());
            }
        }

        //imprimir uma cartela
        static void ImprimirCartelas(int[,] cartela)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (cartela[i, j] == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    if (cartela[i, j] < 10 && cartela[i, j] >= 0)
                    {
                        Console.Write(cartela[i, j] + " |");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(cartela[i, j] + "|");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }

        //gerar cartelas diferentes
        class GeradorCartelas
        {
            private Cartela[] cartelas;
            private Random random = new Random();

            //gerará a quantidade de cartelas que o usuário solicitou
            public GeradorCartelas(int quantidade)
            {
                if (quantidade < 1 || quantidade > 4)
                {
                    Console.WriteLine("\nQuantidade inválida de cartelas");
                }

                cartelas = new Cartela[quantidade];
                //chama a função CentroCartelasMesmo pois preciso garantir que na verificação o centro sempre seja -1
                for (int i = 0; i < quantidade; i++)
                {
                    cartelas[i] = new Cartela();
                    CentroCartelasMesmo(cartelas[i]);
                }

                CartelaUnica();
            }
            //compara uma cartela e outra se elas são diferentes, alterando a comparação dentro de um comando de repetição
            private void CartelaUnica()
            {
                for (int i = 0; i < cartelas.Length; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        while (VerficiarDiferencaCartelas(cartelas[i], cartelas[i - 1]))
                        {
                            cartelas[i] = new Cartela();
                            CentroCartelasMesmo(cartelas[i]);
                        }
                    }
                }
            }
            //garante que o centro de qualquer cartela seja sempre -1, para ser chamado durante as verificações
            private void CentroCartelasMesmo(Cartela cartela)
            {
                cartela.ObterCartela()[2, 2] = -1;
            }
            //método que de fato verifica se os números são iguais, para ser usado dentro dos outros métodos
            private bool VerficiarDiferencaCartelas(Cartela x, Cartela y)
            {
                int[,] m1 = x.ObterCartela();
                int[,] m2 = y.ObterCartela();

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (m1[i, j] == m2[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            //retorna as cartelas geradas
            public Cartela[] ObterNovasCartelas()
            {
                return cartelas;
            }
        }

        class Cartela
        {
            //criando e inicializando a cartela e o gerador de números aleatórios
            private int[,] cartelaBingo = new int[5, 5];
            private Random R = new Random();

            //construtor base
            public Cartela()
            {
                cartelaBingo[2, 2] = -1;

                GerarCartela();
            }

            //construtor com matriz
            public Cartela(int[,] matriz)
            {
                cartelaBingo = matriz;
            }
            //método para gerar minha cartela e verificar se o número colocado não se repete
            private void GerarCartela()
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        //verificar se é repetido através de outro método
                        if (cartelaBingo[i, j] != -1)
                        {
                            int aleatorio;
                            //o número gerado irá entrar primeiro no método para gerá-lo de acordo com a coluna, depois
                            //o número gerado irá entrar no método "Repetido", se ele for igual a algum número já presente na matriz
                            //o método retornará true, assim, não sairá do while até que o método retorne false
                            do
                            {
                                aleatorio = EspecificoColuna(j);
                            } while (Repetido(aleatorio));

                            cartelaBingo[i, j] = aleatorio;
                        }
                    }
                }
            }
            //método para gerar os números na faixa especificada para cada coluna
            private int EspecificoColuna(int coluna)
            {
                switch (coluna)
                {
                    case 0:
                        return R.Next(1, 16);
                        break;
                    case 1:
                        return R.Next(16, 31);
                        break;
                    case 2:
                        return R.Next(32, 46);
                        break;
                    case 3:
                        return R.Next(46, 61);
                        break;
                    case 4:
                        return R.Next(61, 76);
                        break;
                    default:
                        return 0;
                }
            }


            //método para verificar a existência de números repetidos
            private bool Repetido(int numero)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (cartelaBingo[i, j] == numero)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            //retorna a cartela caso seja necessário ela no main
            public int[,] ObterCartela()
            {
                return cartelaBingo;
            }
        }

        class Roleta
        {
            private Random sorteado = new Random();
            private int[] numeroSorteado = new int[76];
            private int posicao = 0;

            //Gira a roleta, verifica o repetido através do método, caso seja repetido ele sorteia outro
            //caso não seja, ela salva esse número no vetor de sorteados, aumenta a posição do vetor e retorna o numero sorteado
            public int GirarRoleta()
            {
                int escolhido;

                do
                {
                    escolhido = sorteado.Next(1, 76);
                } while (JaSorteados(escolhido));

                numeroSorteado[posicao] = escolhido;
                posicao++;
                return escolhido;
            }

            //verificar repetição de números sorteados
            private bool JaSorteados(int x)
            {
                foreach (int num in numeroSorteado)
                {
                    if (num == x)
                    {
                        return true;
                    }
                }
                return false;
            }
            //retorna o vetor de números já sorteados para verificar depois
            public int[] VetorSorteados()
            {
                return numeroSorteado;
            }
        }

        class Jogadores
        {
            public string Nome;
            public int Idade;
            public string Sexo;
            public bool Jogando = true;

            public Cartela[] cartelasOriginais;
            public Cartela[] cartelasCopias;

            public Jogadores(string nome, int idade, string sexo, int quantCartelas)
            {
                Nome = nome;
                Idade = idade;
                Sexo = sexo;

                //inicializa as cartelas originais
                cartelasOriginais = new Cartela[quantCartelas];
                for (int i = 0; i < quantCartelas; i++)
                {
                    cartelasOriginais[i] = new Cartela();
                }

                //inicializa as cópias
                cartelasCopias = new Cartela[quantCartelas];
                for (int i = 0; i < quantCartelas; i++)
                {
                    cartelasCopias[i] = new Cartela(CopiarCartela(cartelasOriginais[i].ObterCartela()));
                }

            }

            //método para criar cópias de cada cartela
            static int[,] CopiarCartela(int[,] cartelaOriginal)
            {
                int[,] copia = new int[5, 5];

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        copia[i, j] = cartelaOriginal[i, j];
                    }
                }
                return copia;
            }

            //método para remover as cartelas erradas
            public void CartelaErrada(int numeroCartela)
            {
                if (numeroCartela >= 1 && numeroCartela <= cartelasCopias.Length)
                {
                    Cartela[] novoVetorCartelas = new Cartela[cartelasCopias.Length - 1];
                    //cria um novo vetor com as cartelas restantes para substituir o antigo vetor de cartelas
                    for (int i = 0, j = 0; i < cartelasCopias.Length; i++)
                    {
                        if (i + 1 != numeroCartela)
                        {
                            novoVetorCartelas[j] = cartelasCopias[i];
                            j++;
                        }
                    }

                    cartelasCopias = novoVetorCartelas;
                }
            }

            public void ExcluirJogador()
            {
                Jogando = false;
            }
        }

        class Players
        {
            public Jogadores[] Jogando;
            private int contador = 0;

            public Players(int tamanho)
            {
                Jogando = new Jogadores[tamanho];
            }

            public void AdicionarJogador(Jogadores jogador)
            {
                if (contador < Jogando.Length)
                {
                    Jogando[contador] = jogador;
                    contador++;
                }
                else
                {
                    Console.WriteLine("\nLimite de Jogadores atingido");
                }
            }

            public void ExcluirJogador(Jogadores jogador)
            {
                for (int i = 0; i < Jogando.Length; i++)
                {
                    if (Jogando[i] == jogador)
                    {
                        Jogando[i].ExcluirJogador();
                        break;
                    }
                }
            }
        }
    }
}
