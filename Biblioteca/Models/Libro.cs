﻿namespace Biblioteca.Models
{
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        
        public int EstadoId { get; set; }
                
    }
}
