using System.Collections;
using NUnit.Framework;
using Zega.Tests.Fuse.JsonFormat;

namespace Zega.Tests
{
    [TestFixture]
    public class FuseTests
    {
        [Test, TestCaseSource(nameof(GenerateFuseTestCases))]
        public void FuseTestCases(FuseTestCase testCase, FuseExpectedCase expectedCase)
        {
            Assert.Fail("Not implemented yet");

            
        }

        public static IEnumerable GenerateFuseTestCases()
        {
            var testCases = FuseTestCase.FromJson(File.ReadAllText("Fuse/JsonFormat/In.json"));
            var expectedCases = FuseExpectedCase.FromJson(File.ReadAllText("Fuse/JsonFormat/Expected.json"));

            for (int i = 0; i < testCases.Count; i++)
            {
                var testCase = testCases[i];
                var expectedCase = expectedCases[i];

                yield return new TestCaseData(testCase, expectedCase)
                    .SetName(testCase.TestDescription);
            }
        }
    }
}
