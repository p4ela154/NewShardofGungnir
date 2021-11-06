using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.GUI;
using Jotunn.Managers;
using Jotunn.Utils;
//using JotunnModExample.ConsoleCommands;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = Jotunn.Logger;
using static EffectList;

//Thanks to MarcoPogo!

namespace ShardOfGungnir
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("cinnabun.backpacks-v1.0.0", BepInDependency.DependencyFlags.SoftDependency)]
    internal class ShardOfGungnir : BaseUnityPlugin
    {
        // BepInEx' plugin metadata
        public const string PluginGUID = "com.jotunn.ShardOfGungnir";
        public const string PluginName = "ShardOfGungnir";
        public const string PluginVersion = "1.0.0";
        private Texture2D CIT1;
        private Sprite CIS1;


        private void Awake()
        {  
            AddEchoOfGungnir(); // Load, create and init your custom mod stuff
            PrefabManager.OnVanillaPrefabsAvailable += AddEchoOfGungnir; // Add custom items cloned from vanilla items
            AddShadowOfGungnir(); // Load, create and init your custom mod stuff
            PrefabManager.OnVanillaPrefabsAvailable += AddShadowOfGungnir; // Add custom items cloned from vanilla items
            AddShardOfGungnir(); // Load, create and init your custom mod stuff
            PrefabManager.OnVanillaPrefabsAvailable += AddShardOfGungnir; // Add custom items cloned from vanilla items
            AddEnchShardOfGungnir(); // Load, create and init your custom mod stuff
            PrefabManager.OnVanillaPrefabsAvailable += AddEnchShardOfGungnir; // Add custom items cloned from vanilla items
        }
        private void AddSpecialEffects(GameObject SE_GameObject)
        {
            //  Try using TargetParentPath = "attach
            KitbashManager.Instance.AddKitbash(SE_GameObject, new KitbashConfig
            {
                KitbashSources = new List<KitbashSourceConfig>
                    {
                        new KitbashSourceConfig
                        {
                            Name = "SE",
                            SourcePrefab = "fx_Lightning",
                            SourcePath = "Sparcs",
                            TargetParentPath = "attach",
                            Position = new Vector3(0, 0, -0.75f),
                            Rotation = Quaternion.Euler(-0, 0, 0),
                            Scale = new Vector3(0.1f, 0.1f, 0.1f)
                        }
                        ,new KitbashSourceConfig
                        {
                            Name = "SE2",
                            SourcePrefab = "fx_Lightning",
                            SourcePath = "Sparcs",
                            //TargetParentPath = "attach", // ONLY FOR DROPPED ITEM
                            Position = new Vector3(0, 0, 0.7f),
                            Rotation = Quaternion.Euler(-0, 0, 0),
                            Scale = new Vector3(0.4f, 0.4f, 0.4f)
                        }
                        ,new KitbashSourceConfig
                        {
                            Name = "SE2",
                            SourcePrefab = "fx_Lightning",
                            SourcePath = "Sparcs",
                            TargetParentPath = "itemdrop",
                            Position = new Vector3(0, 0, 0.7f),
                            Rotation = Quaternion.Euler(-0, 0, 0),
                            Scale = new Vector3(3f, 3f, 3f)
                        }
                        ,new KitbashSourceConfig
                        {
                            Name = "SE3", SourcePrefab = "fx_Lightning", SourcePath = "sfx",  //TargetParentPath = "attach", // ONLY FOR DROPPED ITEM
                            Position = new Vector3(0, 0, -0.7f), Rotation = Quaternion.Euler(-0, 0, 0), Scale = new Vector3(0.1f, 0.1f, 0.1f)

                        }
                        ,new KitbashSourceConfig
                        {
                            Name = "SE4", SourcePrefab = "fx_Lightning", SourcePath = "Point light (1)", TargetParentPath = "attach",
                            Position = new Vector3(0, 0, -0.7f), Rotation = Quaternion.Euler(-0, 0, 0), Scale = new Vector3(0.1f, 0.1f, 0.1f)
                        }
                    }

            }
            );
        }
        private void AddSpecialEffects_projectile(GameObject SE_GameObject)
        {
            //  Try using TargetParentPath = "attach
            KitbashManager.Instance.AddKitbash(SE_GameObject, new KitbashConfig
            {
                Layer = "piece",
                KitbashSources = new List<KitbashSourceConfig>
                    {
                        new KitbashSourceConfig
                        {
                            Name = "SE",
                            SourcePrefab = "fx_Lightning",
                            SourcePath = "Sparcs",
                            Position = new Vector3(0, 0, 0.7f),
                            Rotation = Quaternion.Euler(-0, 0, 0),
                            Scale = new Vector3(1f, 1f, 1f)
                        }
                        ,new KitbashSourceConfig
                        {
                            Name = "SE",
                            SourcePrefab = "fx_Lightning",
                            SourcePath = "sfx",
                            Position = new Vector3(0, 0, 0.7f),
                            Rotation = Quaternion.Euler(-0, 0, 0),
                            Scale = new Vector3(0.7f, 0.7f, 0.7f)

                        }
                        ,new KitbashSourceConfig
                        {
                            Name = "SE",
                            SourcePrefab = "fx_Lightning",
                            SourcePath = "Point light (1)",
                            Position = new Vector3(0, 0, 0.7f),
                            Rotation = Quaternion.Euler(-0, 0, 0),
                            Scale = new Vector3(0.4f, 0.4f, 0.4f)
                        }
                    }
            });
        }
        // Implementation of cloned items
        private void AddEchoOfGungnir()
        {
            try
            {
                CustomItem CI1 = new CustomItem("EchoOfGungnir", "SpearElderbark");
                CustomItem CIP1 = new CustomItem("EchoOfGungnir_projectile", "ancientbarkspear_projectile");
                Texture2D CIT1 = AssetUtils.LoadTexture("ShardOfGungnir/Assets/EchoOfGungnir.png");
                //Sprite CIS1 = new Sprite.Create(CIT1,new Rect(0f,0f, CIT1.width, CIT1.height),Vector2.zero);
                AddSpecialEffects(CI1.ItemDrop.gameObject);
                AddSpecialEffects_projectile(CIP1.ItemPrefab);
                ItemManager.Instance.AddItem(CI1);
                var itemDrop_1 = CI1.ItemDrop;
                var itemDrop_Projectile1 = CIP1.ItemPrefab;
                itemDrop_1.m_itemData.m_shared.m_name = "$item_EchoOfGungnir";
                itemDrop_1.m_itemData.m_shared.m_description = "$item_EchoOfGungnir_desc";
                itemDrop_1.m_itemData.m_shared.m_secondaryAttack.m_attackProjectile = itemDrop_Projectile1;
                CI1.ItemDrop.m_autoPickup = true;
                itemDrop_1.m_itemData.m_shared.m_damages.m_pierce = 35f;
                itemDrop_1.m_itemData.m_shared.m_damages.m_lightning = 5f;
                Sprite CIS1 = AssetUtils.LoadSprite("ShardOfGungnir/Assets/EchoOfGungnir.png"); // Get an icon somehow
                itemDrop_1.GetComponent<ItemDrop>().m_itemData.m_shared.m_icons[0] = CIS1;
                RecipeEchoOfGungnir(itemDrop_1);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Error while adding cloned item: {ex.Message}");
            }
            finally
            {
                // You want that to run only once, Jotunn has the item cached for the game session
                PrefabManager.OnVanillaPrefabsAvailable -= AddEchoOfGungnir;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Implementation of assets via using manual recipe creation and prefab cache's
        private void RecipeEchoOfGungnir(ItemDrop itemDrop_1)
        {
            // Create and add a recipe for the copied item
            Recipe recipe1 = ScriptableObject.CreateInstance<Recipe>();
            recipe1.name = "Recipe_EchoOfGungnir";
            recipe1.m_item = itemDrop_1;
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe1.m_repairStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe1.m_minStationLevel = 1;
            recipe1.m_resources = new Piece.Requirement[]
            {
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("SurtlingCore"),
                    m_amount = 1
                },/*
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("FineWood"),
                    m_amount = 10
                },
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("Bronze"),
                    m_amount = 10
                },*/
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("TrophyEikthyr"),
                    m_amount = 1
                }
            };
            // Since we got the vanilla prefabs from the cache, no referencing is needed
            CustomRecipe CR1 = new CustomRecipe(recipe1, fixReference: false, fixRequirementReferences: false);
            ItemManager.Instance.AddRecipe(CR1);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private void AddShadowOfGungnir()
        {
            try
            {
                CustomItem CI2 = new CustomItem("ShadowOfGungnir", "SpearElderbark");
                CustomItem CIP2 = new CustomItem("ShadowOfGungnir_projectile", "ancientbarkspear_projectile");
                AddSpecialEffects(CI2.ItemDrop.gameObject);
                AddSpecialEffects_projectile(CIP2.ItemPrefab);
                ItemManager.Instance.AddItem(CI2);
                var itemDrop_2 = CI2.ItemDrop;
                var itemDrop_Projectile2 = CIP2.ItemPrefab;
                itemDrop_2.m_itemData.m_shared.m_name = "$item_ShadowOfGungnir";
                itemDrop_2.m_itemData.m_shared.m_description = "$item_ShadowOfGungnir_desc";
                itemDrop_2.m_itemData.m_shared.m_secondaryAttack.m_attackProjectile = itemDrop_Projectile2;
                CI2.ItemDrop.m_autoPickup = true;
                Sprite CIS2 = AssetUtils.LoadSprite("ShardOfGungnir/Assets/ShadowOfGungnir.png"); // Get an icon somehow
                itemDrop_2.GetComponent<ItemDrop>().m_itemData.m_shared.m_icons[0] = CIS2;
                itemDrop_2.m_itemData.m_shared.m_damages.m_pierce = 55f;
                itemDrop_2.m_itemData.m_shared.m_damages.m_lightning = 10f;
                RecipeShadowOfGungnir(itemDrop_2);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Error while adding cloned item: {ex.Message}");
            }
            finally
            {
                // You want that to run only once, Jotunn has the item cached for the game session
                PrefabManager.OnVanillaPrefabsAvailable -= AddShadowOfGungnir;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Implementation of assets via using manual recipe creation and prefab cache's
        private void RecipeShadowOfGungnir(ItemDrop itemDrop_2)
        {
            // Create and add a recipe for the copied item
            Recipe recipe2 = ScriptableObject.CreateInstance<Recipe>();
            recipe2.name = "Recipe_ShadowOfGungnir";
            recipe2.m_item = itemDrop_2;
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe2.m_repairStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe2.m_minStationLevel = 2;
            recipe2.m_resources = new Piece.Requirement[]
            {
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("SurtlingCore"),
                    m_amount = 1
                },
                /*new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("Iron"),
                    m_amount = 5
                },
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("TrophyTheElder"),
                    m_amount = 1
                },*/
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("EchoOfGungnir"),
                    //m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("TrophyEikthyr"),
                    m_amount = 1
                }
            };
            // Since we got the vanilla prefabs from the cache, no referencing is needed
            CustomRecipe CR2 = new CustomRecipe(recipe2, fixReference: false, fixRequirementReferences: false);
            ItemManager.Instance.AddRecipe(CR2);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private void AddShardOfGungnir()
        {
            try
            {
                CustomItem CI3 = new CustomItem("ShardOfGungnir", "SpearElderbark");
                CustomItem CIP3 = new CustomItem("ShardOfGungnir_projectile", "ancientbarkspear_projectile");
                AddSpecialEffects(CI3.ItemDrop.gameObject);
                AddSpecialEffects_projectile(CIP3.ItemPrefab);
                ItemManager.Instance.AddItem(CI3);
                var itemDrop_3 = CI3.ItemDrop;
                var itemDrop_Projectile3 = CIP3.ItemPrefab;
                itemDrop_3.m_itemData.m_shared.m_name = "$item_ShardOfGungnir";
                itemDrop_3.m_itemData.m_shared.m_description = "$item_ShardOfGungnir_desc";
                itemDrop_3.m_itemData.m_shared.m_secondaryAttack.m_attackProjectile = itemDrop_Projectile3;
                Sprite CIS3 = AssetUtils.LoadSprite("ShardOfGungnir/Assets/ShardOfGungnir.png"); // Get an icon somehow
                itemDrop_3.GetComponent<ItemDrop>().m_itemData.m_shared.m_icons[0] = CIS3;
                CI3.ItemDrop.m_autoPickup = true;
                itemDrop_3.m_itemData.m_shared.m_damages.m_pierce = 70f;
                itemDrop_3.m_itemData.m_shared.m_damages.m_lightning = 15f;
                RecipeShardOfGungnir(itemDrop_3);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Error while adding cloned item: {ex.Message}");
            }
            finally
            {
                // You want that to run only once, Jotunn has the item cached for the game session
                PrefabManager.OnVanillaPrefabsAvailable -= AddShardOfGungnir;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Implementation of assets via using manual recipe creation and prefab cache's
        private void RecipeShardOfGungnir(ItemDrop itemDrop_3)
        {
            // Create and add a recipe for the copied item
            Recipe recipe3 = ScriptableObject.CreateInstance<Recipe>();
            recipe3.name = "Recipe_ShardOfGungnir";
            recipe3.m_item = itemDrop_3;
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe3.m_repairStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe3.m_minStationLevel = 2;
            recipe3.m_resources = new Piece.Requirement[]
            {
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("SurtlingCore"),
                    m_amount = 1
                },
                /*new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("Silver"),
                    m_amount = 5
                },
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("TrophyBonemass"),
                    m_amount = 1
                },*/
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("ShadowOfGungnir"),
                    m_amount = 1
                }
            };
            // Since we got the vanilla prefabs from the cache, no referencing is needed
            CustomRecipe CR3 = new CustomRecipe(recipe3, fixReference: false, fixRequirementReferences: false);
            ItemManager.Instance.AddRecipe(CR3);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        private void AddEnchShardOfGungnir()
        {
            try
            {
                CustomItem CI4 = new CustomItem("EnchShardOfGungnir", "SpearElderbark");
                CustomItem CIP4 = new CustomItem("EnchShardOfGungnir_projectile", "ancientbarkspear_projectile");
                AddSpecialEffects(CI4.ItemDrop.gameObject);
                AddSpecialEffects_projectile(CIP4.ItemPrefab);
                ItemManager.Instance.AddItem(CI4);
                var itemDrop_ = CI4.ItemDrop;
                var itemDrop_Projectile = CIP4.ItemPrefab;
                itemDrop_.m_itemData.m_shared.m_name = "$item_EnchShardOfGungnir";
                itemDrop_.m_itemData.m_shared.m_description = "$item_EnchShardOfGungnir_desc";
                itemDrop_.m_itemData.m_shared.m_secondaryAttack.m_attackProjectile = itemDrop_Projectile;
                Sprite CIS4 = AssetUtils.LoadSprite("ShardOfGungnir/Assets/EnchShardOfGungnir.png"); // Get an icon somehow
                itemDrop_.GetComponent<ItemDrop>().m_itemData.m_shared.m_icons[0] = CIS4;
                CI4.ItemDrop.m_autoPickup = true;
                itemDrop_.m_itemData.m_shared.m_damages.m_pierce = 90f;
                itemDrop_.m_itemData.m_shared.m_damages.m_lightning = 20f;
                RecipeEnchShardOfGungnir(itemDrop_);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Error while adding cloned item: {ex.Message}");
            }
            finally
            {
                // You want that to run only once, Jotunn has the item cached for the game session
                PrefabManager.OnVanillaPrefabsAvailable -= AddEnchShardOfGungnir;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Implementation of assets via using manual recipe creation and prefab cache's
        private void RecipeEnchShardOfGungnir(ItemDrop itemDrop_)
        {
            // Create and add a recipe for the copied item
            Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = "Recipe_EnchShardOfGungnir";
            recipe.m_item = itemDrop_;
            recipe.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe.m_repairStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench"); //forge
            recipe.m_minStationLevel = 2;
            recipe.m_resources = new Piece.Requirement[]
            {
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("SurtlingCore"),
                    m_amount = 1
                },
                /*new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("BlackMetal"),
                    m_amount = 5
                },
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("TrophyDragonQueen"),
                    m_amount = 1
                },*/
                new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("ShardOfGungnir"),
                    m_amount = 1
                }
            };
            // Since we got the vanilla prefabs from the cache, no referencing is needed
            CustomRecipe CR4 = new CustomRecipe(recipe, fixReference: false, fixRequirementReferences: false);
            ItemManager.Instance.AddRecipe(CR4);
        }
    }
}
