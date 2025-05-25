using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.Interfaces;
using SWTarefas.Application.UsesCases.TarefasUseCasesSite.ViewModel;

namespace SWTarefas.Site.Controllers
{
    public class TarefasController : Controller
    {
        private readonly IReadTarefasUseCaseSite _readTarefasUseCaseSite;
        private readonly IWriteTarefasUseCaseSite _writeTarefasUseCaseSite;

        public TarefasController(IReadTarefasUseCaseSite readTarefasUseCaseSite, IWriteTarefasUseCaseSite writeTarefasUseCaseSite)
        {
            _readTarefasUseCaseSite = readTarefasUseCaseSite;
            _writeTarefasUseCaseSite = writeTarefasUseCaseSite;
        }

        public IActionResult Index(int status, string titulo = "", DateOnly? dataPrevista = null, DateOnly? dataRealizada = null
            , int dataPrevistaOrd = 0, int dataRealizadaOrd = 0)
        {
            var listaTarefas = responseTarefasFilterIndex(status, titulo, dataPrevista, dataRealizada, dataPrevistaOrd, dataRealizadaOrd);

            return View(listaTarefas);
        }

        private enum Sorting
        {
            Ascend = 1,
            Descend = 2
        }

        private IEnumerable<TarefaViewModel>? responseTarefasFilterIndex(int status, string titulo = "", DateOnly? dataPrevista = null, DateOnly? dataRealizada = null
            , int dataPrevistaOrd = 0, int dataRealizadaOrd = 0)
        {
            var listaTarefasModel = _readTarefasUseCaseSite.GetAll(x => (status > 0 ? x.Status == status : true)
                && (!string.IsNullOrEmpty(titulo.Trim()) ? x.Titulo.Contains(titulo.Trim()) : true)
                && ((dataPrevista != null) ? x.DataConclusaoPrevista == dataPrevista : true)
                && ((dataRealizada != null) ? x.DataConclusaoRealizada == dataRealizada : true)
                );

            if (listaTarefasModel == null)
                return null;

            if (dataPrevistaOrd > 0)
            {
                switch (dataPrevistaOrd)
                {
                    case (int)Sorting.Ascend:
                        listaTarefasModel = listaTarefasModel.OrderByDescending(x => x.Status).OrderBy(x => x.DataConclusaoPrevista);

                        break;
                    case (int)Sorting.Descend:
                        listaTarefasModel = listaTarefasModel.OrderByDescending(x => x.Status).OrderByDescending(x => x.DataConclusaoPrevista);

                        break;
                }
            }

            if (dataRealizadaOrd > 0)
            {
                switch (dataRealizadaOrd)
                {
                    case (int)Sorting.Ascend:
                        listaTarefasModel = listaTarefasModel.OrderByDescending(x => x.Status).OrderBy(x => x.DataConclusaoRealizada);

                        break;
                    case (int)Sorting.Descend:
                        listaTarefasModel = listaTarefasModel.OrderByDescending(x => x.Status).OrderByDescending(x => x.DataConclusaoRealizada);

                        break;
                }
            }

            return listaTarefasModel;
        }

        public async Task<IActionResult> Details(int id, CancellationToken token = default)
        {
            var tarefaViewModel = await _readTarefasUseCaseSite.GetById(id, token);

            if (tarefaViewModel == null)
                return NotFound();

            return View(tarefaViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarefaId,Titulo,Descricao,DataConclusaoPrevista")] TarefaViewModel tarefa, CancellationToken token = default)
        {
            if (ModelState.IsValid)
            {
                await _writeTarefasUseCaseSite.Create(tarefa, token);

                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken token = default)
        {
            var tarefaViewModel = await _readTarefasUseCaseSite.GetById(id, token);

            if (tarefaViewModel == null)
                return NotFound();

            return View(tarefaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarefaId,Titulo,Descricao,DataConclusaoPrevista,DataConclusaoRealizada,Status")] TarefaViewModel tarefa, CancellationToken token = default)
        {
            if (id != tarefa.TarefaId)
                return NotFound();

            if (tarefa.DataConclusaoRealizada != null)
                if (tarefa.DataConclusaoRealizada < tarefa.DataConclusaoPrevista)
                    ModelState.AddModelError("DataConclusaoRealizada", "A data de conclusão realizada tem que ser superior ou igual a data de conclusão prevista.");

            if (ModelState.IsValid)
            {
                try
                {
                    await _writeTarefasUseCaseSite.Update(tarefa, token);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TarefaExists(tarefa.TarefaId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteAjax(int id, CancellationToken token = default)
        {
            var tarefa = await _readTarefasUseCaseSite.GetById(id);

            if (tarefa != null)
                await _writeTarefasUseCaseSite.Delete(id, token);

            return Json(true);
        }

        private async Task<bool> TarefaExists(int id)
        {
            return (await _readTarefasUseCaseSite.GetById(id) != null);
        }
    }
}
