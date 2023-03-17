using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConnectionInfo
{
    public static string ip = "127.0.0.1";
    public static string uid = "root";
    public static string pwd = "12345";
    public static string database = "gamedb";
}

public class DbManager : MonoBehaviour
{
    static string connectionString = $"server = {ConnectionInfo.ip}; uid = {ConnectionInfo.uid}; pwd = {ConnectionInfo.pwd}; Database = {ConnectionInfo.database}; SSLMode = none";

    public static MySqlConnection con;
    private void Awake()
    {
        con = new MySqlConnection(connectionString);
        try
        {
            con.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }    

    public void closeCon()
    {
        con.Close();
    }

    #region Player
    public int InsertToPlayers(string Name, int balance)
    {
        string query = $"insert into gamedb.players (p_name, balance) values ('{Name}',{balance})";

        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();
        return Convert.ToInt32(command.LastInsertedId);
    }

    public List<string> SelectFromPlayers()
    {
        string query = $"select p_name from gamedb.players";
        List<string> nickList = new List<string>();
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nickList.Add(reader.GetString("p_name"));
                }
                /* reader.Read();*/
                /*Debug.Log(reader.GetString("name"));*/
                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return nickList;
    }

    public int UpdatePlayerBalance(PlayerData playerData)
    {
        string query = $"UPDATE gamedb.players SET balance = {playerData.money} where p_name='{playerData.Name}'";

        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();
        return Convert.ToInt32(command.LastInsertedId);
    }

    public int SelectBalancePlayer(PlayerData playerData)
    {
        int balance = -1;
        string query = $"select balance from gamedb.players where p_name='{playerData.Name}'";
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                balance = reader.GetInt32("balance");
                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return balance;
    }

    /*    public int SelectIdPlayer(string playerName)
        {
            string query = $"select idPlayers from gamedb.players where gamedb.players.p_name={playerName}";
            MySqlCommand command = new MySqlCommand(query, con);
            int id = -1;
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    id =  reader.GetInt32("idPlayers");
                    *//* reader.Read();*//*
                    Debug.Log(reader.GetInt32("idPlayers"));
                    command.Dispose();
                }
                else
                {
                    command.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                command.Dispose();
                Debug.LogError(ex.Message);
            }
            return id;
        }*/
    #endregion


    #region AllChars
    public void IsertIntoChars(PlayerData playerData)
    {

        for (int i = 0; i < playerData.allCharCards.Count; i++)
        {

            string critChance = playerData.allCharCards[i].critChance.ToString();
            string crit = playerData.allCharCards[i].critNum.ToString();
            if (crit != "0")
            {
                crit = crit.Replace(",", ".");
            }
            if (critChance != "0")
            {
                critChance = critChance.Replace(",", ".");
            }
            string query = $"insert into gamedb.characters(char_name, race, class, rarity, desctiption, health, speed ,p_attack,m_attack,char_range,p_defence,m_defence,crit_possibility,crit,passive,special_1,special_2,special_3,path) values('{playerData.allCharCards[i].name}','{playerData.allCharCards[i].race}','{playerData.allCharCards[i].Class}','{playerData.allCharCards[i].rarity}','{playerData.allCharCards[i].description}',{playerData.allCharCards[i].health},{playerData.allCharCards[i].speed},{playerData.allCharCards[i].physAttack},{playerData.allCharCards[i].magAttack},{playerData.allCharCards[i].range},{playerData.allCharCards[i].physDefence},{playerData.allCharCards[i].magDefence},{critChance},{crit},'{playerData.allCharCards[i].passiveAbility}','{playerData.allCharCards[i].attackAbility}','{playerData.allCharCards[i].defenceAbility}','{playerData.allCharCards[i].buffAbility}','{playerData.allCharCards[i].image.name}')";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }

    public List<Card> SelectFromChars()
    {
        string query = $"select * from gamedb.characters";
        List<Card> cardList = new List<Card>();
        MySqlCommand command = new MySqlCommand(query, con);

        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card item = new Card();
                    item.name = reader.GetString("char_name");

                    switch (reader.GetString("race"))
                    {
                        case "Люди":
                            item.race = enums.Races.Люди;
                            break;
                        case "Гномы":
                            item.race = enums.Races.Гномы;
                            break;
                        case "Эльфы":
                            item.race = enums.Races.Эльфы;
                            break;
                        case "ТёмныеЭльфы":
                            item.race = enums.Races.ТёмныеЭльфы;
                            break;
                        case "МагическиеСущества":
                            item.race = enums.Races.МагическиеСущества;
                            break;
                    }

                    switch (reader.GetString("class"))
                    {
                        case "Паладин":
                            item.Class = enums.Classes.Паладин;
                            break;
                        case "Лучник":
                            item.Class = enums.Classes.Лучник;
                            break;
                        case "Кавалерия":
                            item.Class = enums.Classes.Кавалерия;
                            break;
                        case "Маг":
                            item.Class = enums.Classes.Маг;
                            break;
                    }

                    switch (reader.GetString("rarity"))
                    {
                        case "Обычная":
                            item.rarity = enums.Rarity.Обычная;
                            break;
                        case "Мифическая":
                            item.rarity = enums.Rarity.Мифическая;
                            break;
                    }

                    item.description = reader.GetString("desctiption");

                    item.health = Convert.ToInt32(reader.GetFloat("health"));
                    item.speed = Convert.ToInt32(reader.GetFloat("speed"));
                    item.physAttack = Convert.ToDouble(reader.GetFloat("p_attack"));
                    item.magAttack = Convert.ToDouble(reader.GetFloat("m_attack"));
                    item.range = Convert.ToInt32(reader.GetFloat("char_range"));
                    item.physDefence = Convert.ToDouble(reader.GetFloat("p_defence"));
                    item.magDefence = Convert.ToDouble(reader.GetFloat("m_defence"));
                    item.critChance = Convert.ToDouble(reader.GetDouble("crit_possibility"));
                    item.critNum = Convert.ToDouble(reader.GetDouble("crit"));

                    item.passiveAbility = reader.GetString("passive");
                    item.attackAbility = reader.GetString("special_1");
                    item.defenceAbility = reader.GetString("special_2");
                    item.buffAbility = reader.GetString("special_3");

                    item.Price = reader.GetInt32("price");

                    item.image = Resources.Load<Sprite>($"Card images/card of char/{reader.GetString("path")}") ;
                    item.id = reader.GetInt32("idCharacters");
                    
/*                    Debug.Log($"name {item.name} ({reader.GetString("char_name")})");
                    Debug.Log($"раса {item.race} ({reader.GetString("race")})");
                    Debug.Log($"class {item.Class} ({reader.GetString("class")})");
                    Debug.Log($"rarity {item.rarity} ({reader.GetString("rarity")})");
                    Debug.Log($"desctiption {item.description} ({reader.GetString("desctiption")})");
                    Debug.Log($"health {item.health} ({reader.GetFloat("health")})");
                    Debug.Log($"speed {item.speed} ({reader.GetFloat("speed")})");
                    Debug.Log($"physAttack {item.physAttack} ({reader.GetFloat("p_attack")})");
                    Debug.Log($"magAttack {item.magAttack} ({reader.GetFloat("m_attack")})");
                    Debug.Log($"range {item.range} ({reader.GetFloat("char_range")})");
                    Debug.Log($"physDefence {item.physDefence} ({reader.GetFloat("p_defence")})");
                    Debug.Log($"magDefence {item.magDefence} ({reader.GetFloat("m_defence")})");
                    Debug.Log($"critChance {item.critChance} ({reader.GetDouble("crit_possibility")})");
                    Debug.Log($"critNum {item.critNum} ({reader.GetDouble("crit")})");
                    Debug.Log($"passiveAbility {item.passiveAbility} ({reader.GetString("passive")})");
                    Debug.Log($"attackAbility {item.attackAbility} ({reader.GetString("special_1")})");
                    Debug.Log($"defenceAbility {item.defenceAbility} ({reader.GetString("special_2")})");
                    Debug.Log($"buffAbility {item.buffAbility} ({reader.GetString("special_3")})");
                    Debug.Log($"name {item.image}");    
 */

                    cardList.Add(item);
                }

                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return cardList;
    }
    #endregion


    #region allSupport
    public void IsertIntoCardsSupport(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allSupportCards.Count; i++)
        {
            string query = $"insert into gamedb.cards(race,name,effect,type,rarity,path, price) values('{playerData.allSupportCards[i].race}','{playerData.allSupportCards[i].name}','{playerData.allSupportCards[i].ability}','{playerData.allSupportCards[i].type}','{playerData.allSupportCards[i].rarity}','{playerData.allSupportCards[i].name}',{playerData.allSupportCards[i].Price})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }
    public List<cardSupport> SelectFromCardsSupport()
    {
        string query = $"select * from gamedb.cards";
        List<cardSupport> cardSupportList = new List<cardSupport>();
        MySqlCommand command = new MySqlCommand(query, con);

        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cardSupport item = new cardSupport();
                    item.name = reader.GetString("name");
                    switch (reader.GetString("race"))
                    {
                        case "Люди":
                            item.race = enums.Races.Люди;
                            break;
                        case "Гномы":
                            item.race = enums.Races.Гномы;
                            break;
                        case "Эльфы":
                            item.race = enums.Races.Эльфы;
                            break;
                        case "ТёмныеЭльфы":
                            item.race = enums.Races.ТёмныеЭльфы;
                            break;
                        case "МагическиеСущества":
                            item.race = enums.Races.МагическиеСущества;
                            break;
                    }
                    switch (reader.GetString("type"))
                    {
                        case "атакующая":
                            item.type = enums.typeOfSupport.атакующая;
                            break;
                        case "защитная":
                            item.type = enums.typeOfSupport.защитная;
                            break;
                        case "мобильность":
                            item.type = enums.typeOfSupport.мобильность;
                            break;
                    }
                    item.ability = reader.GetString("effect");
                    switch (reader.GetString("rarity"))
                    {
                        case "Обычная":
                            item.rarity = enums.Rarity.Обычная;
                            break;
                        case "Мифическая":
                            item.rarity = enums.Rarity.Мифическая;
                            break;
                    }
                    item.image =Resources.Load<Sprite>($"Card images/cards of support/{reader.GetString("path")}");
                   /* Debug.Log($"image {item.image}");*/

                   item.id = reader.GetInt32("idCards");
                    item.Price = reader.GetInt32("price");
                    /*                    Debug.Log($"имя {item.name} ({reader.GetString("name")}), раса {item.race} ({reader.GetString("race")}), способность {item.ability} ({reader.GetString("effect")}), тип {item.type} ({reader.GetString("type")}), редкость {item.rarity} ({reader.GetString("rarity")})");*/
                    cardSupportList.Add(item);
                }

                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return cardSupportList;
    }



    #endregion


    #region supportShop


    public void InsertToCardsSupportShop(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allShopSupportCards.Count; i++)
        {
            string query = $"insert into gamedb.cards_shop(idCards_Shop,cost,id_player) values({playerData.allShopSupportCards[i].id},{playerData.allShopSupportCards[i].Price},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }
    public void RemoveCardsSupportShop(PlayerData playerData)
    {
        string query = $"DELETE FROM gamedb.cards_shop WHERE id_player = {playerData.PlayerId}";
        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();
       
    }


    public void InsertToCardsSupportShopStart(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allSupportCards.Count - 7; i++)
        {
            string query = $"insert into gamedb.cards_shop(idCards_Shop,cost,id_player) values({playerData.allSupportCards[i].id},{playerData.allSupportCards[i].Price},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }

    public List<cardSupport> SelectFromCardsSupportShop(PlayerData playerData)
    {
        string query = $"SELECT * FROM gamedb.cards as c inner join gamedb.cards_shop as cs on(c.idCards = cs.idCards_Shop) where cs.id_player = {playerData.PlayerId}";
        List<cardSupport> cardSupportList = new List<cardSupport>();
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cardSupport item = new cardSupport();
                    item.name = reader.GetString("name");
                    switch (reader.GetString("race"))
                    {
                        case "Люди":
                            item.race = enums.Races.Люди;
                            break;
                        case "Гномы":
                            item.race = enums.Races.Гномы;
                            break;
                        case "Эльфы":
                            item.race = enums.Races.Эльфы;
                            break;
                        case "ТёмныеЭльфы":
                            item.race = enums.Races.ТёмныеЭльфы;
                            break;
                        case "МагическиеСущества":
                            item.race = enums.Races.МагическиеСущества;
                            break;
                    }
                    switch (reader.GetString("type"))
                    {
                        case "атакующая":
                            item.type = enums.typeOfSupport.атакующая;
                            break;
                        case "защитная":
                            item.type = enums.typeOfSupport.защитная;
                            break;
                        case "мобильность":
                            item.type = enums.typeOfSupport.мобильность;
                            break;
                    }
                    item.ability = reader.GetString("effect");
                    switch (reader.GetString("rarity"))
                    {
                        case "Обычная":
                            item.rarity = enums.Rarity.Обычная;
                            break;
                        case "Мифическая":
                            item.rarity = enums.Rarity.Мифическая;
                            break;
                    }
                    item.Price = reader.GetInt32("price");
                    item.id = reader.GetInt32("idCards");
/*                    Debug.Log($"имя {item.name} ({reader.GetString("name")}), раса {item.race} ({reader.GetString("race")}), способность {item.ability} ({reader.GetString("effect")}), тип {item.type} ({reader.GetString("type")}), редкость {item.rarity} ({reader.GetString("rarity")})");
*/                    cardSupportList.Add(item);
                }

                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return cardSupportList;

    }
    #endregion


    #region charShop

    public void InsertToCardsShop(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allShopCharCards.Count; i++)
        {
            string query = $"insert into gamedb.characters_shop(idCharacters_Shop,cost,id_payer) values({playerData.allShopCharCards[i].id},{playerData.allShopCharCards[i].Price},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }
    public void RemoveCardsShop(PlayerData playerData)
    {
        string query = $"DELETE FROM gamedb.characters_shop WHERE id_payer = {playerData.PlayerId}";
        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();

    }






    public void InsertToCardsShopStart(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allCharCards.Count - 5; i++)
        {
            string query = $"insert into gamedb.characters_shop(idCharacters_Shop,cost,id_payer) values({playerData.allCharCards[i].id},{playerData.allCharCards[i].Price},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }

    public List<Card> SelectFromCardsShop(PlayerData playerData)
    {
        string query = $"SELECT * FROM gamedb.characters as c inner join gamedb.characters_shop as cs on(c.idCharacters = cs.idCharacters_Shop) where cs.id_payer = {playerData.PlayerId}";
        List<Card> cardList = new List<Card>();
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card item = new Card();
                    item.name = reader.GetString("char_name");

                    switch (reader.GetString("race"))
                    {
                        case "Люди":
                            item.race = enums.Races.Люди;
                            break;
                        case "Гномы":
                            item.race = enums.Races.Гномы;
                            break;
                        case "Эльфы":
                            item.race = enums.Races.Эльфы;
                            break;
                        case "ТёмныеЭльфы":
                            item.race = enums.Races.ТёмныеЭльфы;
                            break;
                        case "МагическиеСущества":
                            item.race = enums.Races.МагическиеСущества;
                            break;
                    }

                    switch (reader.GetString("class"))
                    {
                        case "Паладин":
                            item.Class = enums.Classes.Паладин;
                            break;
                        case "Лучник":
                            item.Class = enums.Classes.Лучник;
                            break;
                        case "Кавалерия":
                            item.Class = enums.Classes.Кавалерия;
                            break;
                        case "Маг":
                            item.Class = enums.Classes.Маг;
                            break;
                    }

                    switch (reader.GetString("rarity"))
                    {
                        case "Обычная":
                            item.rarity = enums.Rarity.Обычная;
                            break;
                        case "Мифическая":
                            item.rarity = enums.Rarity.Мифическая;
                            break;
                    }

                    item.description = reader.GetString("desctiption");

                    item.health = Convert.ToInt32(reader.GetFloat("health"));
                    item.speed = Convert.ToInt32(reader.GetFloat("speed"));
                    item.physAttack = Convert.ToDouble(reader.GetFloat("p_attack"));
                    item.magAttack = Convert.ToDouble(reader.GetFloat("m_attack"));
                    item.range = Convert.ToInt32(reader.GetFloat("char_range"));
                    item.physDefence = Convert.ToDouble(reader.GetFloat("p_defence"));
                    item.magDefence = Convert.ToDouble(reader.GetFloat("m_defence"));
                    item.critChance = Convert.ToDouble(reader.GetDouble("crit_possibility"));
                    item.critNum = Convert.ToDouble(reader.GetDouble("crit"));

                    item.passiveAbility = reader.GetString("passive");
                    item.attackAbility = reader.GetString("special_1");
                    item.defenceAbility = reader.GetString("special_2");
                    item.buffAbility = reader.GetString("special_3");

                    item.Price = reader.GetInt32("price");

                    item.image = Resources.Load<Sprite>($"Card images/card of char/{reader.GetString("path")}");
                    item.id = reader.GetInt32("idCharacters");
                    cardList.Add(item);
                }

                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return cardList;

    }
    #endregion


    #region supportOwn


    public void InsertToCardsSupportOwn(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allUserSupportCards.Count; i++)
        {
            string query = $"insert into gamedb.owned_cards(card_id,player_id) values({playerData.allUserSupportCards[i].id},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }
    public void RemoveCardsSupportOwn(PlayerData playerData)
    {
        string query = $"DELETE FROM gamedb.owned_cards WHERE player_id = {playerData.PlayerId}";
        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();

    }



    public void InsertToOwnCardsSupportStart(PlayerData playerData)
    {
        for (int i = playerData.allSupportCards.Count - 7; i < playerData.allSupportCards.Count ; i++)
        {
            string query = $"insert into gamedb.owned_cards(card_id,player_id) values({playerData.allSupportCards[i].id},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }

    public List<cardSupport> SelectFromOwnCardsSupport(PlayerData playerData)
    {
        string query = $"SELECT * FROM gamedb.cards as c inner join gamedb.owned_cards as cs on(c.idCards = cs.card_id) where cs.player_id = {playerData.PlayerId}";
        List<cardSupport> cardSupportList = new List<cardSupport>();
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cardSupport item = new cardSupport();
                    item.name = reader.GetString("name");
                    switch (reader.GetString("race"))
                    {
                        case "Люди":
                            item.race = enums.Races.Люди;
                            break;
                        case "Гномы":
                            item.race = enums.Races.Гномы;
                            break;
                        case "Эльфы":
                            item.race = enums.Races.Эльфы;
                            break;
                        case "ТёмныеЭльфы":
                            item.race = enums.Races.ТёмныеЭльфы;
                            break;
                        case "МагическиеСущества":
                            item.race = enums.Races.МагическиеСущества;
                            break;
                    }
                    switch (reader.GetString("type"))
                    {
                        case "атакующая":
                            item.type = enums.typeOfSupport.атакующая;
                            break;
                        case "защитная":
                            item.type = enums.typeOfSupport.защитная;
                            break;
                        case "мобильность":
                            item.type = enums.typeOfSupport.мобильность;
                            break;
                    }
                    item.ability = reader.GetString("effect");
                    switch (reader.GetString("rarity"))
                    {
                        case "Обычная":
                            item.rarity = enums.Rarity.Обычная;
                            break;
                        case "Мифическая":
                            item.rarity = enums.Rarity.Мифическая;
                            break;
                    }
                    item.Price = reader.GetInt32("price");
                    item.id = reader.GetInt32("idCards");
/*                    Debug.Log($"имя {item.name} ({reader.GetString("name")}), раса {item.race} ({reader.GetString("race")}), способность {item.ability} ({reader.GetString("effect")}), тип {item.type} ({reader.GetString("type")}), редкость {item.rarity} ({reader.GetString("rarity")})");
*/                    cardSupportList.Add(item);
                }

                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return cardSupportList;

    }

    #endregion


    #region charOwn


    public void InsertToCardsOwn(PlayerData playerData)
    {
        for (int i = 0; i < playerData.allUserCharCards.Count; i++)
        {
            string query = $"insert into gamedb.owned_characters(character_id,playerId) values({playerData.allUserCharCards[i].id},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }
    public void RemoveCardsOwn(PlayerData playerData)
    {
        string query = $"DELETE FROM gamedb.owned_characters WHERE playerId = {playerData.PlayerId}";
        var command = new MySqlCommand(query, con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        command.Dispose();

    }




    public void InsertToOwnCardStart(PlayerData playerData)
    {
        for (int i = playerData.allCharCards.Count - 5; i < playerData.allCharCards.Count; i++)
        {
            string query = $"insert into gamedb.owned_characters(character_id,playerId) values({playerData.allCharCards[i].id},{playerData.PlayerId})";
            var command = new MySqlCommand(query, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            command.Dispose();
        }
    }

    public List<Card> SelectFromOwnCards(PlayerData playerData)
    {
        string query = $"SELECT * FROM gamedb.characters as c inner join gamedb.owned_characters as cs on(c.idCharacters = cs.character_id) where cs.playerId = {playerData.PlayerId}";
        List<Card> cardList = new List<Card>();
        MySqlCommand command = new MySqlCommand(query, con);
        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Card item = new Card();
                    item.name = reader.GetString("char_name");

                    switch (reader.GetString("race"))
                    {
                        case "Люди":
                            item.race = enums.Races.Люди;
                            break;
                        case "Гномы":
                            item.race = enums.Races.Гномы;
                            break;
                        case "Эльфы":
                            item.race = enums.Races.Эльфы;
                            break;
                        case "ТёмныеЭльфы":
                            item.race = enums.Races.ТёмныеЭльфы;
                            break;
                        case "МагическиеСущества":
                            item.race = enums.Races.МагическиеСущества;
                            break;
                    }

                    switch (reader.GetString("class"))
                    {
                        case "Паладин":
                            item.Class = enums.Classes.Паладин;
                            break;
                        case "Лучник":
                            item.Class = enums.Classes.Лучник;
                            break;
                        case "Кавалерия":
                            item.Class = enums.Classes.Кавалерия;
                            break;
                        case "Маг":
                            item.Class = enums.Classes.Маг;
                            break;
                    }

                    switch (reader.GetString("rarity"))
                    {
                        case "Обычная":
                            item.rarity = enums.Rarity.Обычная;
                            break;
                        case "Мифическая":
                            item.rarity = enums.Rarity.Мифическая;
                            break;
                    }

                    item.description = reader.GetString("desctiption");

                    item.health = Convert.ToInt32(reader.GetFloat("health"));
                    item.speed = Convert.ToInt32(reader.GetFloat("speed"));
                    item.physAttack = Convert.ToDouble(reader.GetFloat("p_attack"));
                    item.magAttack = Convert.ToDouble(reader.GetFloat("m_attack"));
                    item.range = Convert.ToInt32(reader.GetFloat("char_range"));
                    item.physDefence = Convert.ToDouble(reader.GetFloat("p_defence"));
                    item.magDefence = Convert.ToDouble(reader.GetFloat("m_defence"));
                    item.critChance = Convert.ToDouble(reader.GetDouble("crit_possibility"));
                    item.critNum = Convert.ToDouble(reader.GetDouble("crit"));

                    item.passiveAbility = reader.GetString("passive");
                    item.attackAbility = reader.GetString("special_1");
                    item.defenceAbility = reader.GetString("special_2");
                    item.buffAbility = reader.GetString("special_3");

                    item.Price = reader.GetInt32("price");

                    item.image = Resources.Load<Sprite>($"Card images/card of char/{reader.GetString("path")}");
                    item.id = reader.GetInt32("idCharacters");
                    cardList.Add(item);
                }

                command.Dispose();
            }
            else
            {
                command.Dispose();
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
        }
        return cardList;

    }

    #endregion
}




