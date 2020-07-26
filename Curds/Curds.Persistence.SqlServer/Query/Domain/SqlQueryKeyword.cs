using System.ComponentModel.DataAnnotations;

namespace Curds.Persistence.Query.Domain
{
    public enum SqlQueryKeyword
    {
        NULL,
        INT,

        CREATE,
        DROP,
        TABLE,
        INSERT,
        OUTPUT,
        INTO,
        VALUES,
        SELECT,
        DELETE,
        UPDATE,
        SET,
        FROM,
        JOIN,
        ON,
        WHERE,

        AND,
        OR,
        NOT,

        inserted,

        [Display(Name = "=")]
        Equals,
        [Display(Name = "<>")]
        NotEquals,
        [Display(Name = ">")]
        GreaterThan,
        [Display(Name = ">=")]
        GreaterThanOrEquals,
        [Display(Name = "<")]
        LessThan,
        [Display(Name = "<=")]
        LessThanOrEquals,
        [Display(Name = "%")]
        Modulo,
    }
}
