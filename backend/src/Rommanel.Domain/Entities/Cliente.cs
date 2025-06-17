using Rommanel.Domain.ValueObjects;

namespace Rommanel.Domain.Entities;
public enum TipoPessoa
{
    Fisica = 1,
    Juridica = 2
}

public class Cliente
{
    public Guid Id { get; private set; }
    public  string Nome { get; private set; }
    public string Documento { get; private set; }
    public TipoPessoa TipoPessoa { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public Endereco Endereco { get; private set; }
    public string? InscricaoEstadual { get; private set; }
    public bool IsentoIE { get; private set; }

    protected Cliente() { }

    public Cliente(
        string nome,
        string documento,
        TipoPessoa tipoPessoa,
        DateTime dataNascimento,
        string telefone,
        string email,
        Endereco endereco,
        string? inscricaoEstadual,
        bool isentoIE)
    {
        Nome = nome;
        Documento = documento;
        TipoPessoa = tipoPessoa;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Email = email;
        Endereco = endereco;
        InscricaoEstadual = isentoIE ? null : inscricaoEstadual;
        IsentoIE = isentoIE;
    }
    public static Cliente Create(
    string nome,
    string documento,
    string email,
    string telefone,
    DateTime dataNascimento,
    Endereco endereco,
    string? inscricaoEstadual,
    bool isentoIE)
    {
        var cliente = new Cliente{Id = Guid.NewGuid()};
        cliente.Update(nome, documento, email, telefone, dataNascimento, endereco, inscricaoEstadual, isentoIE);
        return cliente;
    }

    public void Update(
        string nome,
        string documento,
        string email,
        string telefone,
        DateTime dataNascimento,
        Endereco endereco,
        string? inscricaoEstadual,
        bool isentoIE)
    {
        SetName(nome);
        SetDocument(documento);
        SetEmail(email);
        SetPhone(telefone);
        SetBirthDate(dataNascimento);
        SetAddress(endereco);
        SetStateRegistration(inscricaoEstadual);
        SetIsStateRegistrationExempt(isentoIE);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid name.");

        Nome = name;
    }

    public void SetDocument(string document)
    {
        if (string.IsNullOrWhiteSpace(document))
            throw new ArgumentException("Invalid document.");

        if (document.Length == 11)
            TipoPessoa = TipoPessoa.Fisica;
        else if (document.Length == 14)
            TipoPessoa = TipoPessoa.Juridica;
        else
            throw new ArgumentException("Document must be 11 (CPF) or 14 (CNPJ) characters.");

        Documento = document;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Invalid email.");

        Email = email;
    }

    public void SetPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Invalid phone.");

        Telefone = phone;
    }

    public void SetBirthDate(DateTime birthDate)
    {
        if (birthDate > DateTime.Today)
            throw new ArgumentException("Birth date cannot be in the future.");

        DataNascimento = birthDate;
    }

    public void SetAddress(Endereco endereco)
    {
        ArgumentNullException.ThrowIfNull(endereco);

        Endereco = endereco;
    }


    public void SetStateRegistration(string? ie)
    {
        InscricaoEstadual = ie;
    }

    public void SetIsStateRegistrationExempt(bool isExempt)
    {
        IsentoIE = isExempt;
    }
}
