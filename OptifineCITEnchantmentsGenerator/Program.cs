
Console.WriteLine("Enter one gear type (e.g netherite_helmet, diamond_axe, or netherite_armor for whole set)");
string gearType = Console.ReadLine();
string gearTexture = gearType.Split("_")[1];

Console.WriteLine("Enter enchantments (fire_protection,unbreaking) max 3");
string enchantmentsString = Console.ReadLine();
string[] enchantments = enchantmentsString.Split(",");

string enchantmentFileName = GenerateEnchantmentFileName();

int maxEnchantments = 10; // nbt.Enchantments.0 to nbt.Enchantments.9
int counter = 0;
string itemType;

for (int i = 0; i < enchantments.Length; i++)
    enchantments[i] = "minecraft:" + enchantments[i];

if (gearType.Split("_")[1] == "armor")
{
    var gearTier = gearType.Split("_")[0];
    var gears = new string[4];

    gears[0] = $"{gearTier}_helmet";
    gears[1] = $"{gearTier}_chestplate";
    gears[2] = $"{gearTier}_leggings";
    gears[3] = $"{gearTier}_boots";

    foreach (var gear in gears)
    {
        gearType = gear;
        gearTexture = gearType.Split("_")[1];
        GenerateFiles();

        counter = 0;
    }
}
else
{
    GenerateFiles();
}


void GenerateFiles()
{
    if (CheckIfArmor(gearType))
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
    else
    {
        Console.WriteLine("Too many enchantments");
    }
}

void One(string propertyContent)
{
       // Generate property files for all possible combinations
    for (int i = 0; i < maxEnchantments; i++)
    {
        string fileName = $"{gearType}_{itemType}_{i}.properties";

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

            string fileName = $"{gearType}_{itemType}_{i}_{j}.properties";

            string _propertyContent = propertyContent;
            

            _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
            _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";

            counter++;
            string filePath = FilePath();
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

                string fileName = $"{gearType}_{itemType}_{i}_{j}_{k}.properties";

                string _propertyContent = propertyContent;

                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";
                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{k}.id={enchantments[2]}";

                counter++;
                string filePath = FilePath();
                File.WriteAllText($"{filePath}\\{fileName}", _propertyContent);
            }
        }
    }
}

string Item()
{
    var texture = gearTexture;
    if (!CheckIfArmor(gearType))
        texture = gearType;

    string propertyContent = $"type=item{Environment.NewLine}items={gearType}{Environment.NewLine}texture=../{gearTexture}";
    return propertyContent;
}

string Gear()
{
    string gearTexture;
    if (gearType.Contains("leggings", StringComparison.OrdinalIgnoreCase))
        gearTexture = "layer_2";
    else
        gearTexture = "layer_1";

    string gearTypeLayer = gearType.Substring(0, gearType.IndexOf("_"));
    string propertyContent = $"type=armor{Environment.NewLine}items={gearType}{Environment.NewLine}texture.{gearTypeLayer}_{gearTexture}=../{gearTexture}";

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
        string armorType = gearType.Split("_")[1];
        filePath = $"D:\\Program\\Minecraft\\Instances\\1.19.4 New\\resourcepacks\\pack template\\assets\\minecraft\\optifine\\cit\\armor\\{enchantmentFileName}\\{armorType}";
    }
    else if (itemType == "item" && CheckIfArmor(gearType))
    {
        string armorType = gearType.Split("_")[1];
        filePath = $"D:\\Program\\Minecraft\\Instances\\1.19.4 New\\resourcepacks\\pack template\\assets\\minecraft\\optifine\\cit\\item\\gear\\{enchantmentFileName}\\{armorType}";
    }

    else
    {
        filePath = $"D:\\OptifineCITEnchantmentFiles";
    }
    return filePath;
}

string GenerateEnchantmentFileName()
{
    string enchantmentFileName = "";
    foreach (var enchantment in enchantments)
    {
        enchantmentFileName += (enchantment + "_");
    }
    enchantmentFileName = enchantmentFileName.TrimEnd('_');
    return enchantmentFileName;
}