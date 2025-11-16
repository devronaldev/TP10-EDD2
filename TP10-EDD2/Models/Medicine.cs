using System.Text;

namespace TP10_EDD2.Models;

/// <summary>
/// Represents a specific type of medicine, managing its details and inventory (batches).
/// </summary>
/// <remarks>
/// PT-BR: Representa um tipo específico de medicamento, gerenciando seus detalhes e inventário (lotes).
/// </remarks>
public class Medicine
{
    /// <summary>
    /// The unique identifier for the medicine.
    /// </summary>
    /// <remarks>
    /// PT-BR: O identificador único para o medicamento.
    /// </remarks>
    public int Id { get; set; }

    /// <summary>
    /// The commercial name of the medicine.
    /// </summary>
    /// <remarks>
    /// PT-BR: O nome comercial do medicamento.
    /// </remarks>
    public string Name { get; set; }

    /// <summary>
    /// The name of the laboratory that manufactures the medicine.
    /// </summary>
    /// <remarks>
    /// PT-BR: O nome do laboratório que fabrica o medicamento.
    /// </remarks>
    public string LabName { get; set; }

    /// <summary>
    /// A queue of available batches for this medicine, managed in FIFO (First-In, First-Out) order.
    /// </summary>
    /// <remarks>
    /// PT-BR: Uma fila de lotes disponíveis para este medicamento, gerenciada em ordem FIFO (Primeiro que Entra, Primeiro que Sai).
    /// </remarks>
    public Queue<Batch> Batches { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Medicine"/> class, creating an empty batch queue.
    /// </summary>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância da classe <see cref="Medicine"/>, criando uma fila de lotes vazia.
    /// </remarks>
    public Medicine()
    {
        Batches = new Queue<Batch>();
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Medicine"/> class with specific details.
    /// </summary>
    /// <param name="name">The commercial name of the medicine.</param>
    /// <param name="labName">The name of the laboratory.</param>
    /// <param name="id">The optional unique identifier for the medicine. Defaults to 0.</param>
    /// <remarks>
    /// PT-BR: Inicializa uma nova instância da classe <see cref="Medicine"/> com detalhes específicos.
    /// <para>PT-BR (name): O nome comercial do medicamento.</para>
    /// <para>PT-BR (labName): O nome do laboratório.</para>
    /// <para>PT-BR (id): O identificador único opcional para o medicamento. Padrão é 0.</para>
    /// </remarks>
    public Medicine(string name, string labName, int id = 0) : this()
    {
        Id = id;
        Name = name;
        LabName = labName;
    }
    
    /// <summary>
    /// Calculates the total quantity of medicine by summing all batches currently in the queue.
    /// </summary>
    /// <returns>The total quantity available in all batches.</returns>
    /// <remarks>
    /// PT-BR: Calcula a quantidade total de medicamento somando todos os lotes atualmente na fila.
    /// <para>PT-BR (returns): A quantidade total disponível em todos os lotes.</para>
    /// </remarks>
    public int QtyAvailable()
    {
        var sum = 0;
        foreach (var batch in Batches)
        {
            // --- CORREÇÃO APLICADA ---
            sum += batch.Qty;
        }
        return sum;
    }
    
    /// <summary>
    /// Adds a new batch of medicine to the inventory queue. Assigns a new sequential ID to the batch.
    /// </summary>
    /// <param name="batch">The new batch to be added.</param>
    /// <remarks>
    /// PT-BR: Adiciona um novo lote de medicamento à fila de inventário. Atribui um novo ID sequencial ao lote.
    /// <para>PT-BR (batch): O novo lote a ser adicionado.</para>
    /// </remarks>
    public void BuyBatch(Batch batch)
    {
        // --- CORREÇÃO APLICADA ---
        batch.Id = Batches.Any() ? Batches.Max(b => b.Id) + 1 : 1;
        Batches.Enqueue(batch);
    }

    /// <summary>
    /// Sells a specified quantity of medicine, consuming batches in FIFO order.
    /// Automatically removes expired batches before processing the sale.
    /// </summary>
    /// <param name="qty">The quantity of medicine to sell.</param>
    /// <returns>True if the sale was successful, false if the available (non-expired) quantity was insufficient.</returns>
    /// <remarks>
    /// PT-BR: Vende uma quantidade específica de medicamento, consumindo os lotes em ordem FIFO.
    /// Remove automaticamente os lotes vencidos antes de processar a venda.
    /// <para>PT-BR (qty): A quantidade de medicamento a ser vendida.</para>
    /// <para>PT-BR (returns): True se a venda foi bem-sucedida, false se a quantidade disponível (não vencida) foi insuficiente.</para>
    /// </remarks>
    public bool SellMedicine(int qty)
    {
        while (Batches.Count > 0 && Batches.Peek().ExpiryDate < DateTime.Now)
        {
            Batches.Dequeue();
        }

        if (qty > QtyAvailable())
        {
            return false;
        }

        while (qty > 0 && Batches.Count > 0)
        {
            var batch = Batches.Peek();

            if (batch.Qty >= qty)
            {
                batch.Qty -= qty;
                qty = 0;
                if (batch.Qty == 0)
                {
                    Batches.Dequeue();
                }
            }
            else
            {
                qty -= batch.Qty;
                Batches.Dequeue();
            }
        }
    
        return true;
    }

    /// <summary>
    /// Returns a basic string representation of the medicine (ID, name, lab, and total quantity).
    /// </summary>
    /// <returns>A formatted string with summary details.</returns>
    /// <remarks>
    /// PT-BR: Retorna uma representação básica em string do medicamento (ID, nome, laboratório e quantidade total).
    /// <para>PT-BR (returns): Uma string formatada com detalhes resumidos.</para>
    /// </remarks>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"=== {Name} ===");
        sb.AppendLine($"Id: {Id}");
        sb.AppendLine($"Laboratório: {LabName}");
        sb.AppendLine($"Disponível(is): {QtyAvailable()}");
        return sb.ToString();
    }

    /// <summary>
    /// Returns a detailed string representation of the medicine, including all batch information.
    /// </summary>
    /// <returns>A formatted string with summary details and all batch details.</returns>
    /// <remarks>
    /// PT-BR: Retorna uma representação detalhada em string do medicamento, incluindo as informações de todos os lotes.
    /// <para>PT-BR (returns): Uma string formatada com detalhes resumidos e os detalhes de todos os lotes.</para>
    /// </remarks>
    public string ShowBatches()
    {
        StringBuilder sb = new StringBuilder(ToString());
        sb.AppendLine($"Lotes:");
        foreach (Batch batch in Batches)
        {
            sb.AppendLine(batch.ToString());
        }
        return sb.ToString();
    }

    /// <summary>
    /// Checks if this medicine is equal to another medicine by comparing their Id properties.
    /// </summary>
    /// <param name="medicine">The other medicine to compare against.</param>
    /// <returns>True if the IDs are the same, otherwise false.</returns>
    /// <remarks>
    /// PT-BR: Verifica se este medicamento é igual a outro medicamento, comparando suas propriedades Id.
    /// <para>PT-BR (medicine): O outro medicamento para comparação.</para>
    /// <para>PT-BR (returns): True se os IDs forem iguais, caso contrário false.</para>
    /// </remarks>
    public bool Equals(Medicine medicine)
    {
        if (medicine.Id == Id)
        {
            return true;
        }
        return false;
    }
}