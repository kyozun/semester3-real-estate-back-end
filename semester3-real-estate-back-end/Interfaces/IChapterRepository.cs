using System.Net;
using System.Runtime.InteropServices;
using semester4.DTO.Chapter;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IChapterRepository
{
    Task<IEnumerable<Chapter>> GetChapters(ChapterQuery chapterQuery);
    Task<Chapter?> GetChapterById(string chapterId, [Optional] List<ChapterInclude> includes);
    Task<HttpStatusCode> CreateChapter(Chapter chapter);
    Task<HttpStatusCode> UpdateChapter(UpdateChapterDto updateChapterDto);
    Task<HttpStatusCode> DeleteChapter(List<Guid> chapterIds);
    Task<bool> ChapterExistsAsync(string chapterId);
}