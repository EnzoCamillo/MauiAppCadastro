using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;

namespace MauiAppCadastro
{
    public partial class MainPage : ContentPage
    {
        public static List<Produto> Produtos { get; set; } = new List<Produto>();

        public MainPage()
        {
            InitializeComponent();
            Validade.IsVisible = false;
        }

        private void AdicionarProduto_Clicked(object sender, EventArgs e)
        {
            string nome = nomeEntry.Text;
            string descricao = DescricaoEntry.Text;
            string categoria = CategoriaEntry.Text;
            DateTime? validade = null;

            if (possuiValidadeSwitch.IsToggled)
            {
                validade = Validade.Date;
            }

            if (double.TryParse(precoEntry.Text, out double preco) && !string.IsNullOrWhiteSpace(nome))
            {
                MainPage.Produtos.Add(new Produto
                {
                    Nome = nome,
                    Preco = preco,
                    Descricao = descricao,
                    Categoria = categoria,
                    Validade = validade,

                });

                mensagemLabel.Text = "Produto Cadastrado com Sucesso!";
                nomeEntry.Text = string.Empty;
                precoEntry.Text = string.Empty;
                DescricaoEntry.Text = string.Empty;
                CategoriaEntry.Text = string.Empty;
                possuiValidadeSwitch.IsToggled = false;
                Validade.Date = DateTime.Now;
            }
            else
            {
                mensagemLabel.Text = "Preencha os campos corretamente!";
            }
        }

        private async void IrParaLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaProdutosPage());
        }

        private void PossuiValidadeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            Validade.IsVisible = e.Value;
        }

        private void OnEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry && entry.Parent is Border border)
            {
                border.Stroke = Colors.Red;
            }
        }

        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (sender is Entry entry && entry.Parent is Border border)
            {
                border.Stroke = Color.FromArgb("#444");
            }
        }
    }

    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public string Descricao { get; set; }
        public DateTime? Validade { get; set; }
        public string Categoria { get; set; }
        public bool Vencido => Validade.HasValue && Validade.Value < DateTime.Now;

        public string ValidadeFormatada => Validade?.ToString("dd/MM/yyyy") ?? "Sem validade";
        public string PrecoFormatado => $"R$ {Preco:F2}";

        public Produto(string nome, double preco, string descricao, DateTime? validade, string categoria)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome), "O nome não pode ser nulo");
            Preco = preco;
            Descricao = descricao;
            Validade = validade;
            Categoria = categoria;
        }

        public Produto() { }
    }
}
