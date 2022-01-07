namespace PorterGroup.Desafio.Infra.Data.Repositories.Statements
{
    internal static class ApartamentoStmt
    {
        public const string SelectById = @"
            SELECT
	            Id,
                Numero,
                Andar,
                BlocoId
            FROM 
              Apartamento
	        WHERE
              Id = @Id";

        public const string Insert = @"
            INSERT INTO Apartamento ( 
	            Id,
                Numero,
                Andar,
                BlocoId)
            VALUES ( 
	            @Id,
                @Numero,
                @Andar,
                @BlocoId
            )";

        public const string Update = @"
            UPDATE Apartamento SET
                Numero = @Numero,
                Andar = @Andar,
                BlocoId = @BlocoId
            WHERE
              Id = @Id";
    }
}
