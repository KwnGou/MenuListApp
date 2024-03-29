USE [MenuListDB]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IngredientCategory] [int] NOT NULL,
	[Name] [nvarchar](20) COLLATE Latin1_General_CI_AS NOT NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientsCategory]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientsCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) COLLATE Latin1_General_CI_AS NOT NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_IngredientsCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemCategory] [int] NOT NULL,
	[Name] [nvarchar](30) COLLATE Latin1_General_CI_AS NOT NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemsCategory]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemsCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) COLLATE Latin1_General_CI_AS NOT NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_ItemsType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetimeoffset](7) NOT NULL,
	[Plate] [int] NOT NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlateCategory]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlateCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) COLLATE Latin1_General_CI_AS NOT NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_PlateCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlateIngredients]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlateIngredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlateId] [int] NOT NULL,
	[IngredientId] [int] NOT NULL,
 CONSTRAINT [PK_PlateIngredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plates]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlateCategory] [int] NOT NULL,
	[Name] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Recipe] [nvarchar](2000) COLLATE Latin1_General_CI_AS NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_Plates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingList]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetimeoffset](7) NOT NULL,
	[Comments] [nvarchar](max) COLLATE Latin1_General_CI_AS NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_ShoppingList_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShoppingListDetails]    Script Date: 4/18/2023 4:52:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingListDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShoppingListId] [int] NOT NULL,
	[RelatedObjectType] [int] NOT NULL,
	[RelatedObjectId] [int] NOT NULL,
	[Remarks] [nvarchar](100) COLLATE Latin1_General_CI_AS NULL,
	[Rowversion] [timestamp] NULL,
 CONSTRAINT [PK_ShoppingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Ingredients] ON 

INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (2, 1, N'Paprica')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (4, 1, N'Pepper')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (3, 1, N'Salt')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (22, 2, N'Cheese')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (6, 2, N'Milk')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (12, 3, N'Beef')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (13, 3, N'Calf')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (10, 3, N'Chicken')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (11, 3, N'Pork')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (21, 4, N'Bread')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (20, 4, N'Flour')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (16, 4, N'Potatoes')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (15, 4, N'Rice')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (14, 4, N'Spaggetti')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (7, 7, N'Salmon')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (8, 7, N'Tuna')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (23, 8, N'Butter')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (24, 8, N'Oil')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (17, 9, N'Tomatoes')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (18, 10, N'Apples')
INSERT [dbo].[Ingredients] ([Id], [IngredientCategory], [Name]) VALUES (19, 11, N'Eggs')
SET IDENTITY_INSERT [dbo].[Ingredients] OFF
GO
SET IDENTITY_INSERT [dbo].[IngredientsCategory] ON 

INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (4, N'Carbs')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (2, N'Dairy')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (8, N'Fats')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (7, N'Fish')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (10, N'Fruits')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (6, N'Herbs')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (3, N'Meat')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (5, N'Nuts')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (11, N'Protein')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (1, N'Spices')
INSERT [dbo].[IngredientsCategory] ([Id], [Name]) VALUES (9, N'Vegetables')
SET IDENTITY_INSERT [dbo].[IngredientsCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (20, 1, N'Coffe')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (1, 1, N'Coke')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (7, 1, N'Sprite')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (10, 1, N'Tea')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (12, 2, N'Ajax')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (6, 2, N'Bref kitchen')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (11, 2, N'HG Floor')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (18, 2, N'Soap')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (19, 2, N'Towel')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (15, 3, N'Tuppleware')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (17, 5, N'Cake')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (16, 5, N'Milka bar')
INSERT [dbo].[Items] ([Id], [ItemCategory], [Name]) VALUES (9, 6, N'Kitchen Paper')
SET IDENTITY_INSERT [dbo].[Items] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemsCategory] ON 

INSERT [dbo].[ItemsCategory] ([Id], [Name]) VALUES (7, N'Accessoires ')
INSERT [dbo].[ItemsCategory] ([Id], [Name]) VALUES (1, N'Beverages')
INSERT [dbo].[ItemsCategory] ([Id], [Name]) VALUES (2, N'Cleaning products')
INSERT [dbo].[ItemsCategory] ([Id], [Name]) VALUES (3, N'Containers')
INSERT [dbo].[ItemsCategory] ([Id], [Name]) VALUES (6, N'Paper Products')
INSERT [dbo].[ItemsCategory] ([Id], [Name]) VALUES (5, N'Sweets')
SET IDENTITY_INSERT [dbo].[ItemsCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([Id], [Date], [Plate]) VALUES (23, CAST(N'2023-04-19T00:00:00.0000000+02:00' AS DateTimeOffset), 15)
INSERT [dbo].[Menu] ([Id], [Date], [Plate]) VALUES (24, CAST(N'2023-04-20T00:00:00.0000000+02:00' AS DateTimeOffset), 16)
INSERT [dbo].[Menu] ([Id], [Date], [Plate]) VALUES (25, CAST(N'2023-04-21T00:00:00.0000000+02:00' AS DateTimeOffset), 17)
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[PlateCategory] ON 

INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (4, N'Beans')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (11, N'Carbs')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (6, N'Carbs and Fats')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (5, N'Cheat Meal')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (8, N'Fats')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (13, N'Fruits')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (7, N'Protein')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (1, N'Protein and Carbs')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (2, N'Protein and Fats')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (10, N'Sweets')
INSERT [dbo].[PlateCategory] ([Id], [Name]) VALUES (12, N'Vegetables')
SET IDENTITY_INSERT [dbo].[PlateCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[PlateIngredients] ON 

INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (79, 15, 2)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (80, 15, 3)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (81, 15, 4)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (82, 15, 12)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (83, 15, 14)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (84, 15, 17)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (85, 15, 23)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (97, 16, 3)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (98, 16, 4)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (96, 16, 19)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (100, 16, 23)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (99, 16, 24)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (91, 17, 3)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (92, 17, 4)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (93, 17, 10)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (90, 17, 15)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (94, 17, 17)
INSERT [dbo].[PlateIngredients] ([Id], [PlateId], [IngredientId]) VALUES (95, 17, 24)
SET IDENTITY_INSERT [dbo].[PlateIngredients] OFF
GO
SET IDENTITY_INSERT [dbo].[Plates] ON 

INSERT [dbo].[Plates] ([Id], [PlateCategory], [Name], [Recipe]) VALUES (15, 1, N'Pasta with Meatballs', N'Cook the pasta and the meatballs then combine')
INSERT [dbo].[Plates] ([Id], [PlateCategory], [Name], [Recipe]) VALUES (16, 7, N'Omelet', N'Break the eggs mix with spices and cook the mixture')
INSERT [dbo].[Plates] ([Id], [PlateCategory], [Name], [Recipe]) VALUES (17, 1, N'Rice with red sauce chicken', N'Boil the rice , fry the chicken add the rest of the ingredients until u  have nice consistency for the sauce ')
SET IDENTITY_INSERT [dbo].[Plates] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingList] ON 

INSERT [dbo].[ShoppingList] ([Id], [Date], [Comments]) VALUES (27, CAST(N'2023-04-18T00:00:00.0000000+02:00' AS DateTimeOffset), N'Menus from 19 Apr 2023 to 21 Apr 2023')
SET IDENTITY_INSERT [dbo].[ShoppingList] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingListDetails] ON 

INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (118, 27, 1, 2, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (119, 27, 1, 3, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (120, 27, 1, 4, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (121, 27, 1, 10, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (122, 27, 1, 12, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (123, 27, 1, 14, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (124, 27, 1, 15, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (125, 27, 1, 17, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (126, 27, 1, 19, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (127, 27, 1, 23, NULL)
INSERT [dbo].[ShoppingListDetails] ([Id], [ShoppingListId], [RelatedObjectType], [RelatedObjectId], [Remarks]) VALUES (128, 27, 1, 24, NULL)
SET IDENTITY_INSERT [dbo].[ShoppingListDetails] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Ingredients_Unique_Category_Name]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Ingredients_Unique_Category_Name] ON [dbo].[Ingredients]
(
	[IngredientCategory] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_IngredientsCategory_Unique_Name]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_IngredientsCategory_Unique_Name] ON [dbo].[IngredientsCategory]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Items_Unique_Category_Name]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Items_Unique_Category_Name] ON [dbo].[Items]
(
	[ItemCategory] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ItemsCategory_Unique_Name]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ItemsCategory_Unique_Name] ON [dbo].[ItemsCategory]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PlateCategory_Name]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PlateCategory_Name] ON [dbo].[PlateCategory]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlateIngredients_Unique]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PlateIngredients_Unique] ON [dbo].[PlateIngredients]
(
	[PlateId] ASC,
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Plates_Unique_Category_Name]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Plates_Unique_Category_Name] ON [dbo].[Plates]
(
	[PlateCategory] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Unique_ShoppingListIItem]    Script Date: 4/18/2023 4:52:07 PM ******/
CREATE NONCLUSTERED INDEX [IX_Unique_ShoppingListIItem] ON [dbo].[ShoppingListDetails]
(
	[RelatedObjectId] ASC,
	[RelatedObjectType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ingredients]  WITH CHECK ADD  CONSTRAINT [FK_Ingredients_IngredientsCategory] FOREIGN KEY([IngredientCategory])
REFERENCES [dbo].[IngredientsCategory] ([Id])
GO
ALTER TABLE [dbo].[Ingredients] CHECK CONSTRAINT [FK_Ingredients_IngredientsCategory]
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD  CONSTRAINT [FK_Items_Items] FOREIGN KEY([ItemCategory])
REFERENCES [dbo].[ItemsCategory] ([Id])
GO
ALTER TABLE [dbo].[Items] CHECK CONSTRAINT [FK_Items_Items]
GO
ALTER TABLE [dbo].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Plates] FOREIGN KEY([Plate])
REFERENCES [dbo].[Plates] ([Id])
GO
ALTER TABLE [dbo].[Menu] CHECK CONSTRAINT [FK_Menu_Plates]
GO
ALTER TABLE [dbo].[PlateIngredients]  WITH CHECK ADD  CONSTRAINT [FK_PlateIngredients_Ingredients] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredients] ([Id])
GO
ALTER TABLE [dbo].[PlateIngredients] CHECK CONSTRAINT [FK_PlateIngredients_Ingredients]
GO
ALTER TABLE [dbo].[PlateIngredients]  WITH CHECK ADD  CONSTRAINT [FK_PlateIngredients_Plates] FOREIGN KEY([PlateId])
REFERENCES [dbo].[Plates] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlateIngredients] CHECK CONSTRAINT [FK_PlateIngredients_Plates]
GO
ALTER TABLE [dbo].[Plates]  WITH CHECK ADD  CONSTRAINT [FK_Plates_PlateCategory] FOREIGN KEY([PlateCategory])
REFERENCES [dbo].[PlateCategory] ([Id])
GO
ALTER TABLE [dbo].[Plates] CHECK CONSTRAINT [FK_Plates_PlateCategory]
GO
ALTER TABLE [dbo].[ShoppingListDetails]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingListDetails_ShoppingList] FOREIGN KEY([ShoppingListId])
REFERENCES [dbo].[ShoppingList] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShoppingListDetails] CHECK CONSTRAINT [FK_ShoppingListDetails_ShoppingList]
GO
