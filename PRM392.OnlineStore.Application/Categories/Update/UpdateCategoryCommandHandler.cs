using MediatR;
using PRM392.OnlineStore.Application.FileUpload;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Categories.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, string>
    {
        private readonly ICategoryRepository _categoryRepository;
    

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }

        public async Task<string> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryExist = await _categoryRepository.FindAsync(x => x.CategoryId == request.ID, cancellationToken);

            if (categoryExist is null )
            {
                return "Category is not found or deleted";
            }



            categoryExist.CategoryName = request.Name ?? categoryExist.CategoryName;
            _categoryRepository.Update(categoryExist);

            return await _categoryRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Update Success!" : "Update Fail!";
        }
    }
}
