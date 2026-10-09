using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NotesFlow.Objects;

namespace NotesFlow.Managers
{
    /// <summary>
    /// Менеджер общения NoetContainer (UI) (временно) и PostgreSQL.
    /// </summary>
    public class NotesApiManager
    {
        private HttpClient _httpClient;
        private string _apiUrl = "http://localhost:5172/api/Notes";
        public NotesApiManager(HttpClient hc) 
            => _httpClient = hc;

        public async Task<List<Note>?> GetNotes()
            => await _httpClient.GetFromJsonAsync<List<Note>>(_apiUrl);

        public async Task<Note?> SaveNote(Note note)
        {
            var response = await _httpClient.PostAsJsonAsync<Note>(_apiUrl, note);
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Note>();
        }

        public async Task<bool> DeleteNote(Guid id)
        {
            var response = await _httpClient.DeleteAsync(_apiUrl + "/" + id.ToString());
            return response.IsSuccessStatusCode;
        }

        public async Task<Note?> UpdateNote(Note note)
        {
            var response = await _httpClient.PutAsJsonAsync<Note>(_apiUrl + "/" + note.Id.ToString(), note);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<Note>();
        }
    }
}
