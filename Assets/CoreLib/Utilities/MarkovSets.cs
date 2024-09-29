using System.Collections.Generic;

namespace CoreLib.Utilities
{
    public static class MarkovSets
    {
        public static List<List<string>> SystemNameData = new List<List<string>>
        {
            new List<string> { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" },
            new List<string> { "Centauri", "Orion", "Pegasus", "Cygnus", "Draco", "Andromeda", "Lyra", "Auriga", "Perseus", "Sagittarius", "Cassiopeia", "Gemini", "Taurus", "Leo", "Virgo", "Libra", "Scorpio", "Capricorn", "Aquarius", "Pisces", "Canis", "Corvus", "Lupus", "Ursa", "Corona", "Hydra", "Octans", "Phoenix", "Vela", "Crux", "Antlia", "Horologium", "Pavo", "Volans", "Cetus", "Dorado", "Grus", "Hydrus", "Indus", "Musca", "Puppis", "Tucana", "Apus", "Chamaeleon", "Eridanus", "Fornax", "Sculptor", "Telescopium", "Virgo" }
        };

        public static List<List<string>> StellarDesignationData = new List<List<string>>
        {
            new List<string> { "GJ", "HD", "HIP", "BD", "2MASS", "NGC", "SAO", "TYC", "HR", "Gliese", "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" },
            new List<string> { "123", "456", "789", "1011", "1213", "1415", "1617", "1819", "2021", "2223", "2425", "2627", "2829", "3031", "3233", "3435", "3637", "3839", "4041", "4243", "4445", "4647", "4849", "5051", "5253", "5455", "5657", "5859", "6061", "6263", "6465", "6667", "6869", "7071", "7273", "7475", "7677", "7879", "8081", "8283", "8485", "8687", "8889", "9091", "9293", "9495", "9697", "9899" }
        };
    }
}