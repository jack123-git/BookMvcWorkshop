using BookApi.Models;
using System.ComponentModel.DataAnnotations;

namespace BookApi.Attibutes
{
    public class BookDataAttribute: ValidationAttribute
    {
        public string GetErrorMessage() => $"借書者 必須要有資料";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var book = validationContext.ObjectInstance as BookData;
            var bookKeeper = book.BOOK_KEEPER;

            return book.BOOK_STATUS switch
            {
                "A" => bookKeeper == "" ? ValidationResult.Success : new ValidationResult(GetErrorMessage()),
                "B" => bookKeeper != "" ? ValidationResult.Success : new ValidationResult(GetErrorMessage()),
                "C" => bookKeeper != "" ? ValidationResult.Success : new ValidationResult(GetErrorMessage()),
                "U" => bookKeeper == "" ? ValidationResult.Success : new ValidationResult(GetErrorMessage()),
            };
        }
    }
}
