using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni.Models
{
    public class Cliente : IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "CPF")]
        [Required(ErrorMessage = "CPF é obrigatório", AllowEmptyStrings = false)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório", AllowEmptyStrings = false)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Informe um nome válido")]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "E-mail é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido")]
        public string Email { get; set; }

        [ForeignKey("Endereco_Id_endereco")]
        public Endereco Endereco { get; set; }

        public int Endereco_Id_endereco { get; set; }

        [ForeignKey("Telefone_Id_telefone")]
        public Telefone Telefone { get; set; }

        public int Telefone_Id_telefone { get; set; }

        public string NomeResumido
        {
            get
            {
                if (string.IsNullOrEmpty(Nome))
                {
                    return null;
                }

                if (Nome.Length > 30)
                {
                    return Nome.Substring(0, 30) + "...";
                }
                else
                {
                    return Nome;
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string Cpf1 = Cpf.Replace(".", "").Replace("-", "");

            if (Cpf1.Length > 11)
                yield return new ValidationResult("Cpf inválido");

            while (Cpf1.Length != 11)
                Cpf1 = '0' + Cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (Cpf1[i] != Cpf1[0])
                    igual = false;

            if (igual || Cpf1 == "12345678909")
                yield return new ValidationResult("Cpf inválido");

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(Cpf1[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    yield return new ValidationResult("Cpf inválido");
            }
            else if (numeros[9] != 11 - resultado)
                yield return new ValidationResult("Cpf inválido");

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    yield return new ValidationResult("Cpf inválido");
            }
            else
                if (numeros[10] != 11 - resultado)
                yield return new ValidationResult("Cpf inválido");
        }
    }
}
