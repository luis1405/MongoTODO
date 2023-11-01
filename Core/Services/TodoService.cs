using MongoTODO.Core.Entities;
using MongoTODO.Repository;

namespace MongoTODO.Core.Services
{
    public class TodoService
    {
        private readonly IMongoRepository<TodoTask> _taskRepository;
        private readonly Response<TodoTask> _response = new Response<TodoTask>();

        public TodoService(IMongoRepository<TodoTask> taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<TodoTask>> GetList()
        {
             return await _taskRepository.GetAll();
        }

        public async Task<Response<TodoTask>> Insert(TodoTask todoTask)
        {
            try
            {
                _response.Object = await _taskRepository.InsertDocument(todoTask);
                if (!string.IsNullOrEmpty(_response.Object.Id))
                    _response.IsSuccess = true;
                else
                {
                    _response.IsSuccess = false;
                    _response.Errors.Add("No se completo la accion");
                }
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.Errors.Add(e.Message);
            }
            return _response;
        }
        public async Task<Response<TodoTask>> Update(TodoTask todoTask)
        {
            try
            {
                _response.Object = await _taskRepository.UpdateDocument(todoTask);
                if(_response.Object != null)
                    _response.IsSuccess = true;
                else
                {
                    _response.IsSuccess = false;
                    _response.Errors.Add("No se encontro el elemento");
                }
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.Errors.Add(e.Message);
            }
            return _response;
        }
        public async Task<Response<TodoTask>> Delete(string id)
        {
            try
            {
                _response.Object = await _taskRepository.DeleteById(id);
                if (_response.Object != null)
                    _response.IsSuccess = true;
                else
                {
                    _response.IsSuccess = false;
                    _response.Errors.Add("No se encontro el elemento");
                }
            }
            catch(Exception e)
            {
                _response.IsSuccess = false;
                _response.Errors.Add(e.Message);
            }
            return _response;
        }
    }
}
