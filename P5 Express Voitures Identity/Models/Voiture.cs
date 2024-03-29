﻿using System.ComponentModel.DataAnnotations;

namespace P5_Express_Voitures_Identity.Models
{
    public class Voiture
    {
        public int Id { get; set; }

        [Display(Name = "Code Vin")]
        public string? CodeVin { get; set; }

        public class AnneeValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                int? annee = value as int?;

                if (annee == null || (annee >= 1990 && annee <= DateTime.Now.Year))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("La date d'achat doit être postérieure à 1990 et inférieur ou égale à l'année en cours");
            }
        }
        [Required(ErrorMessage = "l'année de la  voiture doit être complétée")]
        [RegularExpression("^\\d+$", ErrorMessage = "L'année doit être un entier")]
        [AnneeValidation]
        public int Annee { get; set; }

        [Required(ErrorMessage = "La marque doit être complétée")]
        public string? Marque { get; set; }

        [Required(ErrorMessage = "Le modèle doit être complétée")]
        public string? Modele { get; set; }

        public string? Finition { get; set; }


        public class DateAchatValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                DateTime? date = value as DateTime?;

                if (date == null || (date >= new DateTime(1990, 1, 1) && date <= DateTime.Now))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("La date d'achat doit être postérieure à 1990 et inférieur ou égale à la date du jour");
            }
        }
        [Display(Name = "Date Achat")]
        [Required(ErrorMessage = "la date d'achat de la voiture doit être complétée")]
        [DataType(DataType.Date, ErrorMessage = "La date d'achat doit être une date.")]
        [DateAchatValidation]
        public DateTime DateAchat { get; set; }

        [Display(Name = "Prix Achat")]
        [Required(ErrorMessage = "le prix d'achat de la voiture doit être complétée")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "Le prix d'achat doit être un nombre")]
        public float PrixAchat { get; set; }

        public class DateDisponibiliteALaVenteValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime? date = value as DateTime?;
                if (validationContext == null)
                {
                    throw new ArgumentNullException(nameof(validationContext));
                }

                var dateAchat = (DateTime)validationContext.ObjectInstance.GetType().GetProperty("DateAchat").GetValue(validationContext.ObjectInstance);

                if (date == null || (date >= dateAchat && date <= DateTime.Now.AddYears(1)))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("La date de disponibilité à la vente doit être postérieure ou égale à la date d'achat et inférieur ou égale à la date du jour + 1 an");
            }
        }
        [Display(Name = "Date Disponibilite A La Vente")]
        [DataType(DataType.Date, ErrorMessage = "La date de disponibilité à la vente doit être une date.")]
        [DateDisponibiliteALaVenteValidation]
        public DateTime? DateDisponibiliteALaVente { get; set; }

        [Display(Name = "Prix Vente")]
        [RegularExpression(@"^[0-9]+(\,[0-9]{1,2})?$", ErrorMessage = "Le prix de vente doit être un nombre")]
        public float? PrixVente { get; set; }

        public class DateVenteValidationAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                DateTime? date = value as DateTime?;
                if (validationContext == null)
                {
                    throw new ArgumentNullException(nameof(validationContext));
                }

                if (date == null)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    var dateDisponibiliteALaVente = (DateTime)validationContext.ObjectInstance.GetType().GetProperty("DateDisponibiliteALaVente").GetValue(validationContext.ObjectInstance);

                    if (date == null || (date >= dateDisponibiliteALaVente && date <= DateTime.Now))
                    {
                        return ValidationResult.Success;
                    }
                }
                return new ValidationResult("La date de vente doit être postérieure ou égale à la date de disponibilité à la vente et inférieure ou égale à la date du jour");
            }
        }
        [Display(Name = "Date Vente")]
        [DataType(DataType.Date, ErrorMessage = "La date de vente doit être une date.")]
        [DateVenteValidation]
        public DateTime? DateVente { get; set; }

        public ICollection<Reparation>? Reparations { get; set; }
    }
}
