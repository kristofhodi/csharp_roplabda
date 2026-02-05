using roplabda;

//- A csapattagok.txt állományba mentsük a csapatokat és a hozzájuk tartozó játékosokat a következő formában:
//  Telekom Baku: Yelizaveta Mammadova, Yekaterina Gamova,
//- Állítsa növekvő sorrendbe a posztok szerint a játékosok ösz magasságát
//- Egy szöveges állományba, „alacsonyak.txt” keresse ki a játékosok átlagmagasságától alacsonyabb játékosokat. Az állomány tartalmazza a játékosok nevét, magasságát és hogy mennyivel alacsonyabbak az átlagnál, 2 tizedes pontossággal.

var lines = File.ReadAllLines("adatok.txt");

var players = new List<Player>();

foreach (var line in lines)
{
    var columns = line.Split("\t");
    players.Add(new Player
    {
        Name = columns[0],
        Height = int.Parse(columns[1]),
        Post = columns[2],
        Nationality = columns[3],
        Team = columns[4],
        Country = columns[5],
    });
}

foreach (var player in players)
{
    Console.WriteLine($"{player.Name} -- {player.Height} -- {player.Post} -- {player.Nationality} -- {player.Team} -- {player.Country}");
}

var utok = new List<string>();
foreach (var player in players)
{
    if (player.Post == "ütő")
    {
        utok.Add(player.Name);
    }
}
File.WriteAllLines("utok.txt", utok);

var magas = new List<string>();
foreach (var player in players.OrderByDescending(players => players.Height))
{
    magas.Add($"{player.Name} -- {player.Height}");
}
File.WriteAllLines("magaslatok.txt", magas);

var countries = new Dictionary<string, int>();
foreach (var player in players)
{
    if (countries.ContainsKey(player.Nationality))
    {
        countries[player.Nationality]++;
    } 
    else
    {
        countries[player.Nationality] = 1;
    }
}
File.WriteAllLines("nemzetisegek.txt",
    countries.Select(c => $"{c.Key} -- {c.Value}")
);

var atlag = 0;
var ossz = 0;
foreach (var player in players)
{
    ossz += player.Height;
    atlag = ossz/players.Count;
}
var aboveAvg = new List<string>();
foreach (var player in players)
{
    if (player.Height > atlag)
    {
        aboveAvg.Add($"{player.Name} -- {player.Height}");
    }
}
File.WriteAllLines("atlagnalmagasabbak.txt", aboveAvg);

var belowAvg = new List<string>();
foreach (var player in players)
{
    if (player.Height < atlag)
    {
        belowAvg.Add($"{player.Name} -- {player.Height} -- {atlag - player.Height}");
    }
}
File.WriteAllLines("alacsonyak.txt", belowAvg);
