using semester4.DTO.CustomList;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface ICustomListRepository
{
    Task<IEnumerable<CustomList>> GetCustomLists(CustomListQuery customListQuery);
    Task<CustomList?> GetCustomListById(string customListId);
    Task<CustomList?> CreateCustomList(CustomList customList);
    Task<CustomList?> UpdateCustomList(string customListId, UpdateCustomListDto updateCustomListDto);
    Task<CustomList?> DeleteCustomList(string customListId);
    Task<IEnumerable<CustomList>> GetCustomListsForLoggedUser(string userId);

    Task<IEnumerable<CustomList>> GetCustomListByUserId(string userId);

    Task CreateCustomListManga(string userId, CustomListManga customListManga);

    Task<bool> CustomListExistsAsync(string customListId);
}