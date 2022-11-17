// See https://aka.ms/new-console-template for more informatiova

using SharedObjects.Utils;
using Test;

var entity = new StudentEntity
{
    Number = 10,
    Id = Guid.NewGuid(),
    Date = DateTime.Now,
    Name = "Hung",
    DateTime = DateTime.Now,
};
var model = new StudentModel
{
    Number = 20,
    DateTime = null
};



Console.WriteLine(entity);
ValueExtractor.ExtractValueFromModelToEntity(entity, model);

Console.WriteLine(entity);