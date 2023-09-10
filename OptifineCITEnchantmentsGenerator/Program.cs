

string gearName;
string gearTexture;
string itemType;
int maxEnchantments = 5; // nbt.Enchantments.0 to nbt.Enchantments.9
int counter = 0;
string enchantmentFileName;
string[] enchantments;

while (true)
{
    Console.WriteLine("Enter one gear type (e.g netherite_helmet, diamond_axe, or netherite_armor or netherite_tool for whole set, or sword for all sword tiers)");
    gearName = Console.ReadLine();
    gearTexture = gearName;

    Console.WriteLine("Enter enchantments (fire_protection,unbreaking) max 4");
    string enchantmentsString = Console.ReadLine();
    enchantments = enchantmentsString.Split(",");

    enchantmentFileName = GenerateEnchantmentFileName();



    for (int i = 0; i < enchantments.Length; i++)
        enchantments[i] = "minecraft:" + enchantments[i];

    if (gearName == "sword")
    {
        var swords = new string[6];

        swords[0] = "netherite_sword";
        swords[1] = "diamond_sword";
        swords[2] = "iron_sword";
        swords[3] = "stone_sword";
        swords[4] = "wooden_sword";
        swords[5] = "golden_sword";

        foreach (var sword in swords)
        {
            gearName = sword;
            gearTexture = gearName;
            GenerateFiles();

            counter = 0;
        }
    }
    else if (gearName.Split("_")[1] == "armor")
    {
        var gearTier = gearName.Split("_")[0];
        var gears = new string[4];

        gears[0] = $"{gearTier}_helmet";
        gears[1] = $"{gearTier}_chestplate";
        gears[2] = $"{gearTier}_leggings";
        gears[3] = $"{gearTier}_boots";

        foreach (var gear in gears)
        {
            gearName = gear;
            GenerateFiles();

            counter = 0;
        }
    }
    else if (gearName.Split("_")[1] == "tool")
    {
        var toolTier = gearName.Split("_")[0];
        var tools = new string[4];

        tools[0] = $"{toolTier}_axe";
        tools[1] = $"{toolTier}_pickaxe";
        tools[2] = $"{toolTier}_shovel";
        tools[3] = $"{toolTier}_hoe";

        foreach (var tool in tools)
        {
            gearName = tool;
            GenerateFiles();

            counter = 0;
        }
    }

    else
    {
        GenerateFiles();
    }
    continue;
}



void GenerateFiles()
{
    if (CheckIfArmor(gearName))
    {
        itemType = "armor";
        string content = Gear();
        WriteFiles(content);

        itemType = "item";
        content = Item();
        WriteFiles(content);
    }
    else
    {
        itemType = "item";
        string content = Item();
        WriteFiles(content);
    }
}

void WriteFiles(string content)
{
    if(enchantments.Length == 1)
    {
        One(content);
    }
    else if(enchantments.Length == 2)
    {
        Two(content);
    }
    else if(enchantments.Length == 3)
    {
        Three(content);
    }
    else if(enchantments.Length == 4)
    {
        Four(content);
    }
    else
    {
        Console.WriteLine("Max enchantments is 4");
    }
}

void One(string propertyContent)
{
       // Generate property files for all possible combinations
    for (int i = 0; i < maxEnchantments; i++)
    {
        string fileName = $"{gearName}_{itemType}_{i}.properties";

        string _propertyContent = propertyContent;
        _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";

        counter++;
        string filePath = FilePath();

        Directory.CreateDirectory(filePath);
        File.WriteAllText($"{filePath}\\{fileName}", _propertyContent);
    }
}

void Two(string propertyContent)
{
    // Generate property files for all possible combinations
    for (int i = 0; i < maxEnchantments; i++)
    {
        for (int j = 0; j < maxEnchantments; j++)
        {
            if (i == j)
                continue;

            string fileName = $"{gearName}_{itemType}_{i}_{j}.properties";

            string _propertyContent = propertyContent;
            

            _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
            _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";

            counter++;
            string filePath = FilePath();
            Directory.CreateDirectory(filePath);
            File.WriteAllText($"{filePath}\\{fileName}", _propertyContent);
        }
    }
}

void Three(string propertyContent)
{
    // Generate property files for all possible combinations
    for (int i = 0; i < maxEnchantments; i++)
    {
        for (int j = 0; j < maxEnchantments; j++)
        {
            for(int k = 0;  k < maxEnchantments; k++)
            {
                if(i == j || i == k || j == k)
                    continue;

                string fileName = $"{gearName}_{itemType}_{i}_{j}_{k}.properties";

                string _propertyContent = propertyContent;

                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";
                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{k}.id={enchantments[2]}";

                counter++;
                string filePath = FilePath();
                Directory.CreateDirectory(filePath);
                File.WriteAllText($"{filePath}\\{fileName}", _propertyContent);
            }
        }
    }
}

void Four(string propertyContent)
{
    // Generate property files for all possible combinations
    for (int i = 0; i < maxEnchantments; i++)
    {
        for (int j = 0; j < maxEnchantments; j++)
        {
            for (int k = 0; k < maxEnchantments; k++)
            {
                for (int l = 0; l < maxEnchantments; l++)
                {
                    if (i == j ||
                        i == k ||
                        j == k ||
                        i == l ||
                        j == l ||
                        k == l
                        )
                        continue;

                    string fileName = $"{gearName}_{itemType}_{i}_{j}_{k}_{l}.properties";

                    string _propertyContent = propertyContent;

                    _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
                    _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";
                    _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{k}.id={enchantments[2]}";
                    _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{k}.id={enchantments[3]}";

                    counter++;
                    string filePath = FilePath();
                    Directory.CreateDirectory(filePath);
                    File.WriteAllText($"{filePath}\\{fileName}", _propertyContent);
                }
            }
        }
    }
}

string Item()
{
    string propertyContent = $"type=item{Environment.NewLine}items={gearName}{Environment.NewLine}texture=../{gearTexture}";
    return propertyContent;
}

string Gear()
{
    string gearTexture;
    if (gearName.Contains("leggings", StringComparison.OrdinalIgnoreCase))
        gearTexture = "layer_2";
    else
        gearTexture = "layer_1";

    string gearTierLayer = gearName.Substring(0, gearName.IndexOf("_"));
    string propertyContent = $"type=armor{Environment.NewLine}items={gearName}{Environment.NewLine}texture.{gearTierLayer}_{gearTexture}=../{gearTexture}";

    return propertyContent;
}

bool CheckIfArmor(string gearType)
{
    if (gearType.Contains("helmet", StringComparison.OrdinalIgnoreCase))
        return true;
    else if (gearType.Contains("chestplate", StringComparison.OrdinalIgnoreCase))
        return true;
    else if (gearType.Contains("leggings", StringComparison.OrdinalIgnoreCase))
        return true;
    else if (gearType.Contains("boots", StringComparison.OrdinalIgnoreCase))
        return true;
    else
        return false;
}


string FilePath()
{
    string filePath = "";
    if(itemType == "armor")
    {
        string armorType = gearName.Split("_")[1];
        filePath = $"D:\\Program\\Minecraft\\Instances\\1.19.4 New\\resourcepacks\\pack template\\assets\\minecraft\\optifine\\cit\\armor\\{enchantmentFileName}\\{armorType}";
    }
    else if (itemType == "item" && CheckIfArmor(gearName))
    {
        string armorType = gearName.Split("_")[1];
        filePath = $"D:\\Program\\Minecraft\\Instances\\1.19.4 New\\resourcepacks\\pack template\\assets\\minecraft\\optifine\\cit\\item\\gear\\{enchantmentFileName}\\{armorType}";
    }
    else if(itemType == "item" && !CheckIfArmor(gearName) && gearName.Split("_")[1] == "sword")
    {
        filePath = $"D:\\Program\\Minecraft\\Instances\\1.19.4 New\\resourcepacks\\pack template\\assets\\minecraft\\optifine\\cit\\item\\sword\\{enchantmentFileName}\\{gearName}";
    }
    else if(itemType == "item" && !CheckIfArmor(gearName))
    {
        filePath = $"D:\\Program\\Minecraft\\Instances\\1.19.4 New\\resourcepacks\\pack template\\assets\\minecraft\\optifine\\cit\\item\\tools\\{enchantmentFileName}\\{gearName}";
    }

    return filePath;
}

string GenerateEnchantmentFileName()
{
    string enchantmentFileName = "";

    for (int i = 0; i < enchantments.Length; i++)
        if (i != 0)
            enchantmentFileName += "_";

    foreach (var enchantment in enchantments)
    {
        enchantmentFileName += (enchantment + "_");
    }
    enchantmentFileName = enchantmentFileName.TrimEnd('_');
    return enchantmentFileName;
}