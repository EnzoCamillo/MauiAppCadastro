using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Maui.Storage;
using MauiAppCadastro;

public static class ProdutoStorage
{
    const string ProdutosKey = "ProdutosSalvos";

    public static void SalvarProdutos(List<Produto> produtos)
    { 
        string json = JsonSerializer.Serialize(produtos);

        Preferences.Set(ProdutosKey, json);
    }


    public static List<Produto> CarregarProdutos()
    {
        string json = Preferences.Get(ProdutosKey, string.Empty);

        return string.IsNullOrEmpty(json) ? new List<Produto>() :

        JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
    }
}
