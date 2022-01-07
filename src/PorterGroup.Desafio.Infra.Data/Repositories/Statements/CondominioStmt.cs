namespace PorterGroup.Desafio.Infra.Data.Repositories.Statements
{
    internal static class CondominioStmt
    {
        public const string SelectById = @"
            SELECT
	            Id,
                Nome,
                Telefone,
                EmailSindico
            FROM 
              Condominio
	        WHERE
              Id = @Id";

        public const string Insert = @"
            INSERT INTO Condominio ( 
	            Id,
                Nome,
                Telefone,
                EmailSindico)
            VALUES ( 
	            @Id,
                @Nome,
                @Telefone,
                @EmailSindico
            )";

        public const string Update = @"
            UPDATE Condominio SET
                Nome = @Nome,
                Telefone = @Telefone,
                EmailSindico = @EmailSindico
            WHERE
              Id = @Id";
    }
}
