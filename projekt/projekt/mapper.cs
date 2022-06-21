using AutoMapper;
using projekt.Entity;

namespace projekt
{
    public class mapper:Profile
    {
       public mapper()
        {
            CreateMap<BookDto, Book>().ForMember(b => b.Name, bt => bt.MapFrom(src => $"{src.Name}"))
                .ForMember(b => b.Description, bt => bt.MapFrom(src => $"{src.Description}"))
                .ForMember(b => b.Category, bt => bt.MapFrom(src => new List<Category>() { new Category { Name = src.CName, Description = src.CDescription } }))
                .ForMember(b => b.Authors, bt => bt.MapFrom(src => new List<Author>() { new Author { Name = src.CName, LastName = src.LastName, DateOfBirth = src.DateOfBirth } }));
            CreateMap<UpdateBookDto, Book>().ForMember(b => b.Name, bt => bt.MapFrom(src => $"{src.Name}"))
               .ForMember(b => b.Description, bt => bt.MapFrom(src => $"{src.Description}"));
        }
            
    }
}
