using System;
using ProviderApi.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProviderApi.Models
{
    public class Provider {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Clave primaria auto-generada, no debe venir en el JSON

        [Required(ErrorMessage = "Razón Social Inválida")]
        public string SocialName { get; set; }

        [Required(ErrorMessage = "Nombre Comercial Inválido")]
        public string CommercialName { get; set; }

        [Required(ErrorMessage = "Código de Tributación Inválido")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Debe tener exactamente 11 dígitos.")]
        public string TributeID { get; set; } // Como string para evitar conflicto con ceros al inicio

        [Required(ErrorMessage = "Número de Teléfono Inválido")]
        [RegularExpression(@"^\d{9,15}$", ErrorMessage = "Debe contener solo dígitos.")]
        public string PhoneNumber { get; set; } // para evitar errores con prefijos internacionales

        [Required(ErrorMessage = "Email Inválido")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "El sitio web debe ser una URL válida.")]
        public string WebPage { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "País Inválido")]
        public string Country { get; set; }

        [Required(ErrorMessage = "La facturación anual es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser positivo.")]
        public decimal AnnualBilling { get; set; }

        [Required(ErrorMessage = "La fecha de última edición es obligatoria.")]
        public DateTime LastEdited { get; set; }

        // Constructor (se deben llenar todos los campos)
        public Provider()
        {
            SocialName = string.Empty;
            CommercialName = string.Empty;
            TributeID = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            WebPage = string.Empty;
            Address = string.Empty;
            Country = string.Empty;
            AnnualBilling = 0;
            LastEdited = DateTime.MinValue; 
        }
             
    }
}