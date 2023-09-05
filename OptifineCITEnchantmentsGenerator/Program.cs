
Console.WriteLine("Enter gear type (netherite_helmet, diamond_chestplate)");
string gearType = Console.ReadLine();
string gearTexture = gearType.Split("_")[1];

Console.WriteLine("Enter enchantments (protection,unbreaking)");
string enchantmentsString = Console.ReadLine();
string[] enchantments = enchantmentsString.Split(",");


int maxEnchantments = 10; // nbt.Enchantments.0 to nbt.Enchantments.9
int counter = 0;
string fileNameType;
if (CheckIfArmor(gearType))
{
    fileNameType = "armor";
    string content = Gear();
    WriteFiles(content);

    fileNameType = "item";
    content = Item();
    WriteFiles(content);
}
else
{
    fileNameType = "item";
    string content = Item();
    WriteFiles(content);
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
        string fileName = $"{gearType}_{i}.properties";

        string _propertyContent = propertyContent;
        _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";

        counter++;
        File.WriteAllText(fileName, _propertyContent);
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

            string fileName = $"{gearType}_{fileNameType}_{i}_{j}.properties";

            string _propertyContent = propertyContent;
            

            _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
            _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";

            counter++;
            File.WriteAllText(fileName, _propertyContent);
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

                string fileName = $"{gearType}_{fileNameType}_{i}_{j}_{k}.properties";

                string _propertyContent = propertyContent;

                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{i}.id={enchantments[0]}";
                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{j}.id={enchantments[1]}";
                _propertyContent += $"{Environment.NewLine}nbt.Enchantments.{k}.id={enchantments[2]}";

                counter++;
                File.WriteAllText(fileName, _propertyContent);
            }
        }
    }
}

string Item()
{
    string propertyContent = $"type=item{Environment.NewLine}items={gearType}{Environment.NewLine}texture=./{gearTexture}";
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
    string propertyContent = $"type=armor{Environment.NewLine}items={gearType}{Environment.NewLine}texture.{gearTypeLayer}=./{gearTexture}";

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
