using Domain.Games;

namespace Infrastructure.Games;

public static class GameFaker
{
    public static List<Game> CreateGames() =>
        [
            new()
            {
                Id = new("4ee7459d-fd9e-4b61-b84b-17cf3f9ba60a"),
                Name = "The Legend of Zelda: Tears of the Kingdom",
                Genre = "Adventure",
                Price = 69.99m,
                ReleaseDate = new DateOnly(2023, 5, 12),
            },
            new()
            {
                Id = new("059182b5-446d-4150-bd3d-6b5da8b55462"),
                Name = "Baldur's Gate 3",
                Genre = "RPG",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2023, 8, 3),
            },
            new()
            {
                Id = new("2c9c1e6f-449a-45f8-beb4-263a429ee41d"),
                Name = "Elden Ring",
                Genre = "RPG",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2022, 2, 25),
            },
            new()
            {
                Id = new("b721f457-319a-4319-9aab-908c737fd6ed"),
                Name = "Stardew Valley",
                Genre = "Simulation",
                Price = 14.99m,
                ReleaseDate = new DateOnly(2016, 2, 26),
            },
            new()
            {
                Id = new("92b70f90-5ab9-456d-90b8-845ad89a737f"),
                Name = "Celeste",
                Genre = "Platformer",
                Price = 19.99m,
                ReleaseDate = new DateOnly(2018, 1, 25),
            },
            new()
            {
                Id = new("aafb9348-6071-49e2-b6d8-33617c1f4cfd"),
                Name = "Hades",
                Genre = "Roguelike",
                Price = 24.99m,
                ReleaseDate = new DateOnly(2020, 9, 17),
            },
            new()
            {
                Id = new("9049a9cb-6357-4235-924b-4f16c8b7cb43"),
                Name = "Red Dead Redemption 2",
                Genre = "Adventure",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2018, 10, 26),
            },
            new()
            {
                Id = new("89eae2e1-5633-4521-840e-25797646893d"),
                Name = "Portal 2",
                Genre = "Puzzle",
                Price = 9.99m,
                ReleaseDate = new DateOnly(2011, 4, 18),
            },
            new()
            {
                Id = new("25fa9b67-216e-4f0d-8fa8-45047ed882b7"),
                Name = "Disco Elysium",
                Genre = "RPG",
                Price = 39.99m,
                ReleaseDate = new DateOnly(2019, 10, 15),
            },
            new()
            {
                Id = new("0d2d83dc-74ba-43ac-8529-324fc48df7a6"),
                Name = "Hollow Knight",
                Genre = "Metroidvania",
                Price = 14.99m,
                ReleaseDate = new DateOnly(2017, 2, 24),
            },
            new()
            {
                Id = new("90073eca-5b47-447e-b5d1-dd6ff5fb7542"),
                Name = "Doom Eternal",
                Genre = "Shooter",
                Price = 39.99m,
                ReleaseDate = new DateOnly(2020, 3, 20),
            },
            new()
            {
                Id = new("9320ebf7-dbbd-4e05-b84f-529637fa4422"),
                Name = "Resident Evil 4 Remake",
                Genre = "Horror",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2023, 3, 24),
            },
            new()
            {
                Id = new("6a0237ca-6ce4-4c30-9986-de8d753ed5c1"),
                Name = "God of War Ragnarök",
                Genre = "Adventure",
                Price = 69.99m,
                ReleaseDate = new DateOnly(2022, 11, 9),
            },
            new()
            {
                Id = new("f28fd6f6-4839-4fb5-8f82-8a4312beea0a"),
                Name = "Minecraft",
                Genre = "Sandbox",
                Price = 29.99m,
                ReleaseDate = new DateOnly(2011, 11, 18),
            },
            new()
            {
                Id = new("9a1134ee-48b2-408a-a731-bd3ab569f993"),
                Name = "Cyberpunk 2077",
                Genre = "RPG",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2020, 12, 10),
            },
            new()
            {
                Id = new("630d3fc2-1f38-412e-94a3-1cc7675cbe4b"),
                Name = "It Takes Two",
                Genre = "Cooperative",
                Price = 39.99m,
                ReleaseDate = new DateOnly(2021, 3, 26),
            },
            new()
            {
                Id = new("6964ec77-372f-477e-a553-9b9fffe30208"),
                Name = "Slay the Spire",
                Genre = "Roguelike",
                Price = 24.99m,
                ReleaseDate = new DateOnly(2019, 1, 23),
            },
            new()
            {
                Id = new("fc9885a6-365c-49bb-81ed-afeae7898bf4"),
                Name = "Return of the Obra Dinn",
                Genre = "Puzzle",
                Price = 19.99m,
                ReleaseDate = new DateOnly(2018, 10, 18),
            },
            new()
            {
                Id = new("25ed61a1-f65c-4a46-ab81-b6a73d3c1cc5"),
                Name = "Final Fantasy VII Remake",
                Genre = "RPG",
                Price = 49.99m,
                ReleaseDate = new DateOnly(2020, 4, 10),
            },
            new()
            {
                Id = new("fd5eb5f1-9959-4e45-9717-f670d3d498ea"),
                Name = "Ori and the Will of the Wisps",
                Genre = "Metroidvania",
                Price = 29.99m,
                ReleaseDate = new DateOnly(2020, 3, 11),
            },
            new()
            {
                Id = new("99134b6a-4a16-41e4-9043-96531b63f5f3"),
                Name = "Forza Horizon 5",
                Genre = "Racing",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2021, 11, 9),
            },
            new()
            {
                Id = new("41712794-0526-4b8c-b54a-8be8b56f0cf8"),
                Name = "The Witcher 3: Wild Hunt",
                Genre = "RPG",
                Price = 39.99m,
                ReleaseDate = new DateOnly(2015, 5, 19),
            },
            new()
            {
                Id = new("5bce283e-2c79-493b-a010-7f28a23c6887"),
                Name = "Street Fighter 6",
                Genre = "Fighting",
                Price = 59.99m,
                ReleaseDate = new DateOnly(2023, 6, 2),
            },
            new()
            {
                Id = new("33f97b64-18fc-4a6f-87ca-63d11f046684"),
                Name = "Factorio",
                Genre = "Strategy",
                Price = 35.00m,
                ReleaseDate = new DateOnly(2020, 8, 14),
            },
            new()
            {
                Id = new("99a83674-e8de-4b53-be45-a4b9d1d886f7"),
                Name = "Outer Wilds",
                Genre = "Exploration",
                Price = 24.99m,
                ReleaseDate = new DateOnly(2019, 5, 28),
            },
            new()
            {
                Id = new("b15eec5b-5fa8-4aa6-8470-bf15def986b1"),
                Name = "Dave the Diver",
                Genre = "Adventure",
                Price = 19.99m,
                ReleaseDate = new DateOnly(2023, 6, 28),
            },
            new()
            {
                Id = new("de297e8d-fb11-479f-aef2-3c9ab4d83463"),
                Name = "Vampire Survivors",
                Genre = "Roguelike",
                Price = 4.99m,
                ReleaseDate = new DateOnly(2022, 10, 20),
            },
        ];
}
