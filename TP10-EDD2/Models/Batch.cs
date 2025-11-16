namespace TP10_EDD2.Models;

/// <summary>
/// Represents a single batch of medicine, including its quantity and expiration date.
/// </summary>
/// <remarks>
/// PT-BR: Representa um único lote de medicamento, incluindo sua quantidade e data de validade.
/// </remarks>
public class Batch
{
    /// <summary>
    /// The unique identifier for the batch.
    /// </summary>
    /// <remarks>
    /// PT-BR: O identificador único para o lote.
    /// </remarks>
    public int Id { get; set; }

    /// <summary>
    /// The available quantity of medicine in this batch.
    /// </summary>
    /// <remarks>
    /// PT-BR: A quantidade disponível de medicamento neste lote.
    /// </remarks>
    public int Qty { get; set; }

    /// <summary>
    /// The expiration date of this batch.
    /// </summary>
    /// <remarks>
    /// PT-BR: A data de validade deste lote.
    /// </remarks>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// Initializes a new, empty instance of the <see cref="Batch"/> class.
    /// </summary>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância vazia da classe <see cref="Batch"/>.
    /// </remarks>
    public Batch()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Batch"/> class with specific details.
    /// </summary>
    /// <param name="qty">The quantity of medicine in the batch.</param>
    /// <param name="expiryDate">The expiration date of the batch.</param>
    /// <param name="id">The optional unique identifier for the batch. Defaults to -1.</param>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância da classe <see cref="Batch"/> com detalhes específicos.
    /// <para>PT-BR (qty): A quantidade de medicamento no lote.</para>
    /// <para>PT-BR (expiryDate): A data de validade do lote.</para>
    /// <para>PT-BR (id): O identificador único opcional para o lote. Padrão é -1.</para>
    /// </remarks>
    public Batch(int qty, DateTime expiryDate, int id = -1)
    {
        Id = id;
        Qty = qty;
        ExpiryDate = expiryDate;
    }

    /// <summary>
    /// Returns a string representation of the batch, including its ID, quantity, and expiry date.
    /// </summary>
    /// <returns>A formatted string representing the batch.</returns>
    /// <remarks>
    /// PT-BR: Retorna uma representação em string do lote, incluindo seu ID, quantidade e data de validade.
    /// <para>PT-BR (returns): Uma string formatada representando o lote.</para>
    /// </remarks>
    public override string ToString()
    {
        return $"Id: {Id}, Quantidade: {Qty}, Vencimento: {ExpiryDate}";
    }
}