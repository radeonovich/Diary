using System.Collections.Generic;
using SQLite;
using Diary.DBItemModel;
using System.Threading.Tasks;
using System;

namespace Diary.Data
{
    public class DiaryDB
    {
        readonly SQLiteAsyncConnection db;
        public DiaryDB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);

            db.CreateTableAsync<Note>().Wait(); // создаем таблицу объектов типа Note
        }

        public Task<List<Note>> GetNotesAsync()
        {
            // метод получения всех записей
            return db.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            // получение конкретной записи
            // аналог SQL запроса SELECT FROM Diary WHERE ID=id
            // и выбором первого совпадения
            return db.Table<Note>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync(); 
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            // изменение или добавление записи
            if (note.ID != 0) // запись есть в БД
            {
                // изменяем запись
                return db.UpdateAsync(note);
            }
            else
            {
                // добавляем запись в БД
                return db.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            // удаление записи
            return db.DeleteAsync(note);
        }
    }
}
