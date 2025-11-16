using System.Text;
using TP10_EDD2.Models;

namespace TP10_EDD2.Controllers;

/// <summary>
/// Manages the collection of medicines, handling operations like adding, deleting, and searching.
/// </summary>
/// <remarks>
/// PT-BR: Gerencia a coleção de medicamentos, lidando com operações como adicionar, excluir e pesquisar.
/// </remarks>
public class MedicineController
{
    /// <summary>
    /// The internal list containing all medicine objects managed by this controller.
    /// </summary>
    /// <remarks>
    /// PT-BR: A lista interna contendo todos os objetos de medicamento gerenciados por este controlador.
    /// </remarks>
    private List<Medicine> Medicines { get; set; } = new List<Medicine>();

    /// <summary>
    /// Adds a new medicine to the collection or merges batches if the medicine already exists.
    /// Assigns a new sequential ID if the medicine is new.
    /// </summary>
    /// <param name="medicine">The medicine to add or merge.</param>
    /// <remarks>
    /// PT-BR: Adiciona um novo medicamento à coleção ou mescla os lotes se o medicamento já existir.
    /// Atribui um novo ID sequencial se o medicamento for novo.
    /// <para>PT-BR (medicine): O medicamento a ser adicionado ou mesclado.</para>
    /// </remarks>
    public void AddMedicine(Medicine medicine)
    {
        var existingMedicine = Medicines.FirstOrDefault(m => m.Equals(medicine));
        if (existingMedicine != null)
        {
            while (medicine.Batches.Count > 0)
            {
                existingMedicine.Batches.Enqueue(medicine.Batches.Dequeue());
            }
        }
        else
        {
            // --- CORREÇÃO APLICADA ---
            medicine.Id = Medicines.Any() ? Medicines.Max(m => m.Id) + 1 : 1;
            Medicines.Add(medicine);
        }
    }

    /// <summary>
    /// Deletes a medicine from the collection, only if it exists and has no batches.
    /// </summary>
    /// <param name="medicine">The medicine to delete.</param>
    /// <returns>True if the medicine was successfully deleted, otherwise false.</returns>
    /// <remarks>
    /// PT-BR: Exclui um medicamento da coleção, somente se ele existir e não possuir lotes.
    /// <para>PT-BR (medicine): O medicamento a ser excluído.</para>
    /// <para>PT-BR (returns): True se o medicamento foi excluído com sucesso, caso contrário false.</para>
    /// </remarks>
    public bool DeleteMedicine(Medicine medicine)
    {
        var existingMedicine = Medicines.FirstOrDefault(m => m.Equals(medicine));
        if (existingMedicine != null)
        {
            if (existingMedicine.Batches.Count > 0)
            {
                return false;
            }
            return Medicines.Remove(existingMedicine);
        }
        return false;
    }

    /// <summary>
    /// Searches for a medicine in the collection by its unique ID.
    /// </summary>
    /// <param name="Id">The ID of the medicine to find.</param>
    /// <returns>The found <see cref="Medicine"/> object, or null if not found.</returns>
    /// <remarks>
    /// PT-BR: Procura por um medicamento na coleção pelo seu ID único.
    /// <para>PT-BR (Id): O ID do medicamento a ser encontrado.</para>
    /// <para>PT-BR (returns): O objeto <see cref="Medicine"/> encontrado, ou null se não for encontrado.</para>
    /// </remarks>
    public Medicine? SearchMedicine(int Id)
    {
        return Medicines.FirstOrDefault(m => m.Id == Id);
    }

    /// <summary>
    /// Generates a string report detailing all medicines and their respective batches.
    /// </summary>
    /// <returns>A formatted string containing all medicine and batch information.</returns>
    /// <remarks>
    /// PT-BR: Gera um relatório em string detalhando todos os medicamentos e seus respectivos lotes.
    /// <para>PT-BR (returns): Uma string formatada contendo todas as informações de medicamentos e lotes.</para>
    /// </remarks>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        
        foreach (Medicine medicine in Medicines)
        {
             sb.AppendLine(medicine.ShowBatches());
        }
        return sb.ToString();
    }
}