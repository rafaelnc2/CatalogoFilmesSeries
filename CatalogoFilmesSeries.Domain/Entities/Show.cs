﻿using CatalogoFilmesSeries.Domain.ValueObjects;

namespace CatalogoFilmesSeries.Domain.Entities;

public abstract class Show : Entity
{
    protected List<string> _categorias = new();
    
    public string Titulo { get; protected set; }
    public string TituloOriginal { get; protected set; }
    public int AnoLancamento { get; protected set; }
    public int ClassificacaoEtaria { get; protected set; }
    public string Sinopse { get; protected set; }
    public string UrlImagem { get; protected set; }
    
    //Mudar dto pelo value object
    public ShowInfoVo ShowInfo { get; protected set; }
    
    
    public IReadOnlyList<string> Categorias { get => _categorias; }
    
    
    public void AddCategoria(string categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria) ||
            _categorias.Exists(cat => cat.Equals(categoria, StringComparison.CurrentCultureIgnoreCase)))
        {
            _errors.Add("Categoria nula, em branco ou já incluída.");
            return;
        }
        
        _categorias.Add(categoria);
    }
    
    public void RemoveCategoria(string categoria)
    {
        if (string.IsNullOrWhiteSpace(categoria))
            return;
        
        _categorias.Remove(categoria);  
    } 
    
    public void LimparCategorias() => _categorias.Clear();
}