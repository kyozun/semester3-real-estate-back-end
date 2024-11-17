using semester4.Helpers.Query;

namespace semester4.Interfaces;

public interface IChapterCommentRepository
{
    Task<IEnumerable<ChapterComment>> GetChapterComments(ChapterCommentQuery chapterCommentQuery);
    Task<ChapterComment?> GetChapterCommentById(string chapterCommentId);
    Task<IEnumerable<ChapterComment>> GetChapterCommentsByChapterId(string chapterId);
    Task<IEnumerable<ChapterComment>> GetChapterCommentsByUserId(string userId);
    Task CreateChapterComment(ChapterComment chapterComment);
    Task UpdateChapterComment(ChapterComment chapterComment);
    Task<ChapterComment?> DeleteChapterComment(string chapterCommentId, string userId);
    Task<bool> ChapterCommentExistsAsync(string commentId);
}