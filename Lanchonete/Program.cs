using System;
using System.Collections.Generic;

namespace Lanchonete
{
    class Program
    {
        static List<Produto> produtos = new List<Produto>();
        static List<Pedido> pedidos = new List<Pedido>();

        static void Main(string[] args)
        {
            bool executando = true;
            while (executando)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Lanchonete");
                Console.WriteLine("1. Cadastro de Produto");
                Console.WriteLine("2. Cadastro de Pedido");
                Console.WriteLine("3. Listar Produtos");
                Console.WriteLine("4. Listar Pedidos");
                Console.WriteLine("5. Total de Vendas");
                Console.WriteLine("6. Sair");
                Console.Write("Selecione uma opção: ");

                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        CadastrarProduto();
                        break;
                    case 2:
                        CadastrarPedido();
                        break;
                    case 3:
                        ListarProdutos();
                        break;
                    case 4:
                        ListarPedidos();
                        break;
                    case 5:
                        TotalVendas();
                        break;
                    case 6:
                        executando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }
            }
        }

        static void CadastrarProduto()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de Produto");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            double preco = double.Parse(Console.ReadLine());

            Produto produto = new Produto(nome, preco);
            produtos.Add(produto);

            Console.WriteLine("Produto cadastrado com sucesso!");
            Console.ReadKey();
        }

        static void CadastrarPedido()
        {
            Console.Clear();
            Console.WriteLine("Cadastro de Pedido");
            Pedido pedido = new Pedido();

            bool adicionandoProdutos = true;
            while (adicionandoProdutos)
            {
                Console.Write("Informe o ID ou o nome do produto para adicionar ao pedido (ou 0 para sair): ");
                string input = Console.ReadLine();

                if (input == "0")
                    break;

                Produto produto = null;

                if (int.TryParse(input, out int idProduto))
                {
                    produto = produtos.Find(p => p.Id == idProduto);
                }
                else
                {
                    produto = produtos.Find(p => p.Nome.Equals(input, StringComparison.OrdinalIgnoreCase));
                }

                if (produto != null)
                {
                    pedido.AdicionarProduto(produto);
                    Console.WriteLine("Produto adicionado ao pedido.");
                }
                else
                {
                    Console.WriteLine("Produto não encontrado.");
                }
            }

            pedidos.Add(pedido);
            Console.WriteLine("Pedido cadastrado com sucesso!");
            Console.ReadKey();
        }

        static void ListarProdutos()
        {
            Console.Clear();
            Console.WriteLine("Lista de Produtos");
            foreach (var produto in produtos)
            {
                Console.WriteLine($"{produto.Id}: {produto.Nome} - R$ {produto.Preco}");
            }
            Console.ReadKey();
        }

        static void ListarPedidos()
        {
            Console.Clear();
            Console.WriteLine("Lista de Pedidos");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"Pedido {pedido.Id}:");
                foreach (var item in pedido.Itens)
                {
                    Console.WriteLine($"- {item.Nome} - R$ {item.Preco}");
                }
            }
            Console.ReadKey();
        }

        static void TotalVendas()
        {
            Console.Clear();
            double totalVendas = 0;
            foreach (var pedido in pedidos)
            {
                totalVendas += pedido.Total;
            }
            Console.WriteLine($"Total de Vendas: R$ {totalVendas}");
            Console.ReadKey();
        }
    }

    class Produto
    {
        private static int contador = 1;
        public int Id { get; }
        public string Nome { get; set; }
        public double Preco { get; set; }

        public Produto(string nome, double preco)
        {
            Id = contador++;
            Nome = nome;
            Preco = preco;
        }
    }

    class Pedido
    {
        private static int contador = 1;
        public int Id { get; }
        public List<Produto> Itens { get; }
        public double Total => CalcularTotal();

        public Pedido()
        {
            Id = contador++;
            Itens = new List<Produto>();
        }

        public void AdicionarProduto(Produto produto)
        {
            Itens.Add(produto);
        }

        private double CalcularTotal()
        {
            double total = 0;
            foreach (var item in Itens)
            {
                total += item.Preco;
            }
            return total;
        }
    }
}