using System;
using ProviderApi.Services;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProviderApi.Models
{
    /// <summary>
    /// Representa un proveedor registrado en el sistema.
    /// Contiene datos tributarios, de contacto y financieros.
    /// </summary>
    public class Provider {
        
        /// <summary>
        /// Clave primaria auto-generada. No debe venir en el JSON.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Clave primaria auto-generada, no debe venir en el JSON

        /// <summary>
        /// Razón social del proveedor (nombre legal).
        /// </summary>
        [Required(ErrorMessage = "Razón Social Inválida")]
        public string SocialName { get; set; }

        /// <summary>
        /// Nombre comercial del proveedor.
        /// </summary>
        [Required(ErrorMessage = "Nombre Comercial Inválido")]
        public string CommercialName { get; set; }

        /// <summary>
        /// Número de RUC o código tributario (exactamente 11 dígitos).
        /// </summary>
        [Required(ErrorMessage = "Código de Tributación Inválido")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Debe tener exactamente 11 dígitos.")]
        public string TributeID { get; set; } // Como string para evitar conflicto con ceros al inicio

        /// <summary>
        /// Número de teléfono del proveedor.
        /// </summary>
        [Required(ErrorMessage = "Número de Teléfono Inválido")]
        [RegularExpression(@"^\d{9,15}$", ErrorMessage = "Debe contener solo dígitos.")]
        public string PhoneNumber { get; set; } // para evitar errores con prefijos internacionales

        /// <summary>
        /// Correo electrónico del proveedor.
        /// </summary>
        [Required(ErrorMessage = "Email Inválido")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        public string Email { get; set; }

        /// <summary>
        /// Página web del proveedor (si aplica).
        /// </summary>
        [Url(ErrorMessage = "El sitio web debe ser una URL válida.")]
        public string WebPage { get; set; }

        /// <summary>
        /// Dirección física del proveedor.
        /// </summary>
        [Required(ErrorMessage = "La dirección es obligatoria.")]
        public string Address { get; set; }

        /// <summary>
        /// País donde está registrado el proveedor.
        /// </summary>
        [Required(ErrorMessage = "País Inválido")]
        public string Country { get; set; }

        /// <summary>
        /// Monto de facturación anual del proveedor.
        /// </summary>
        [Required(ErrorMessage = "La facturación anual es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser positivo.")]
        public decimal AnnualBilling { get; set; }

        /// <summary>
        /// Fecha de la última edición del registro del proveedor.
        /// </summary>
        [Required(ErrorMessage = "La fecha de última edición es obligatoria.")]
        public DateTime LastEdited { get; set; }

        /// <summary>
        /// Constructor por defecto. Inicializa todos los campos con valores válidos por defecto.
        /// </summary>
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