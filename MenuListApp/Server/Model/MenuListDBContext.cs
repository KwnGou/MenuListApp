using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MenuListApp.Server.Model
{
    public partial class MenuListDBContext : DbContext
    {
        public MenuListDBContext(DbContextOptions<MenuListDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientsCategory> IngredientsCategories { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<ItemsCategory> ItemsCategories { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Plate> Plates { get; set; } = null!;
        public virtual DbSet<PlateCategory> PlateCategories { get; set; } = null!;
        public virtual DbSet<PlateIngredient> PlateIngredients { get; set; } = null!;
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; } = null!;
        public virtual DbSet<ShoppingListDetail> ShoppingListDetails { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasIndex(e => new { e.IngredientCategory, e.Name }, "IX_Ingredients_Unique_Category_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IngredientCategoryNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.IngredientCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredients_IngredientsCategory");
            });

            modelBuilder.Entity<IngredientsCategory>(entity =>
            {
                entity.ToTable("IngredientsCategory");

                entity.HasIndex(e => e.Name, "IX_IngredientsCategory_Unique_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(25);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasIndex(e => new { e.ItemCategory, e.Name }, "IX_Items_Unique_Category_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ItemCategoryNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ItemCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Items_Items");
            });

            modelBuilder.Entity<ItemsCategory>(entity =>
            {
                entity.ToTable("ItemsCategory");

                entity.HasIndex(e => e.Name, "IX_ItemsCategory_Unique_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(30);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.PlateNavigation)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.Plate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_Plates");
            });

            modelBuilder.Entity<Plate>(entity =>
            {
                entity.HasIndex(e => new { e.PlateCategory, e.Name }, "IX_Plates_Unique_Category_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Recipe).HasMaxLength(2000);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.PlateCategoryNavigation)
                    .WithMany(p => p.Plates)
                    .HasForeignKey(d => d.PlateCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Plates_PlateCategory");
            });

            modelBuilder.Entity<PlateCategory>(entity =>
            {
                entity.ToTable("PlateCategory");

                entity.HasIndex(e => e.Name, "IX_PlateCategory_Name")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<PlateIngredient>(entity =>
            {
                entity.HasIndex(e => new { e.PlateId, e.IngredientId }, "IX_PlateIngredients_Unique")
                    .IsUnique();

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.PlateIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlateIngredients_Ingredients");

                entity.HasOne(d => d.Plate)
                    .WithMany(p => p.PlateIngredients)
                    .HasForeignKey(d => d.PlateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlateIngredients_Plates");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.ToTable("ShoppingList");

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ShoppingListDetail>(entity =>
            {
                entity.HasIndex(e => new { e.RelatedObjectId, e.RelatedObjectType }, "IX_Unique_ShoppingListIItem");

                entity.Property(e => e.Remarks).HasMaxLength(100);

                entity.Property(e => e.Rowversion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.ShoppingList)
                    .WithMany(p => p.ShoppingListDetails)
                    .HasForeignKey(d => d.ShoppingListId)
                    .HasConstraintName("FK_ShoppingListDetails_ShoppingList");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
