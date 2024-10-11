using System;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    // 讓 WebApplication 可以直接呼叫 MigrateDb 方法
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        // 依賴注入 (DI) 容器用來管理應用程式的服務
        using var scope = app.Services.CreateScope();
        // 從 DI 容器中取得 GameStoreContext，這是應用程式的資料庫上下文。
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        // 執行資料庫遷移。此方法會檢查資料庫的結構是否與最新的遷移同步，若不同則應用相應的遷移。
        await dbContext.Database.MigrateAsync();
    }
}
