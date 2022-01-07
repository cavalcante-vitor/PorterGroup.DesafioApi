namespace PorterGroup.Desafio.Infra.Data.Repositories.Statements
{
    internal static class BlocoStmt
    {
        public const string SelectById = @"
            SELECT
	            Id,
                Nome,
                CondominioId
            FROM 
              Bloco
	        WHERE
              Id = @Id";

        public const string Insert = @"
            INSERT INTO Bloco ( 
	            Id,
                Nome,
                CondominioId)
            VALUES ( 
	            @Id,
                @Nome,
                @CondominioId
            )";

        public const string Update = @"
            UPDATE Bloco SET
                Nome = @Nome,
                CondominioId = @CondominioId
            WHERE
              Id = @Id";
    }
}
