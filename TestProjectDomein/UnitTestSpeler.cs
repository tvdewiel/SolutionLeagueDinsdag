using League.BL.Model;

namespace TestProjectDomein
{
    public class UnitTestSpeler
    {
        [Fact]
        public void ZetId_Valid()
        {
            Speler s = new Speler(10,"jos", 180, 87);
            Assert.Equal(10, s.Id);
            s.ZetId(45);
            Assert.Equal(45, s.Id);
        }
    }
}