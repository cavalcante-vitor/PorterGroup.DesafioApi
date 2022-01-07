namespace PorterGroup.Desafio.Infra.Data.Repositories.Statements
{
    internal static class MoradorStmt
    {
        public const string SelectById = @"
            SELECT
	            Id,
                Nome,
                DataNascimento,
                Telefone,
                Cpf,
                Email,
                ApartamentoId
            FROM 
              Morador
	        WHERE
              Id = @Id";

        public const string Insert = @"
            INSERT INTO Morador ( 
	            Id,
                Nome,
                DataNascimento,
                Telefone,
                Cpf,
                Email,
                ApartamentoId)
            VALUES ( 
	            @Id,
                @Nome,
                @DataNascimento,
                @Telefone,
                @Cpf,
                @Email,
                @ApartamentoId
            )";

        public const string Update = @"
            UPDATE Morador SET
                Nome = @Nome,
                DataNascimento = @DataNascimento,
                Telefone = @Telefone,
                Cpf = @Cpf,
                Email = @Email,
                ApartamentoId = @ApartamentoId
            WHERE
                Id = @Id";
    }
}
