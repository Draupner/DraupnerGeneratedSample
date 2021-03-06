using System;
using Library.Core.Common.Transactions;
using Ploeh.AutoFixture;
using Xunit;

namespace Library.Test.Common.Transaction
{
    public class TransactionRangeTests : PersistenceTest, IDisposable
    {
        private readonly FooRepository fooRepository;
        private readonly Fixture fixture;

        public TransactionRangeTests()
        {
            fooRepository = new FooRepository(UnitOfWork);
            fixture = new Fixture();
            TransactionRange.CreateTransactionRangeManager =
                (unitOfWork => new ThreadLocalTransactionRangeManager(unitOfWork));
        }

        [Fact]
        public void ShouldRollbackWhenNoComplete()
        {
            using(new TransactionRange(UnitOfWork))
            {
                var entity = fixture.CreateAnonymous<Foo>();
                fooRepository.Add(entity);
                UnitOfWork.SaveChanges();                
            }

            using (var session = CreateSession())
            {
                var entities = session.QueryOver<Foo>().List();
                Assert.Equal(0, entities.Count);
            }
        }

        [Fact]
        public void ShouldCommit()
        {
            using(var transaction = new TransactionRange(UnitOfWork))
            {
                var entity = fixture.CreateAnonymous<Foo>();
                fooRepository.Add(entity);
                UnitOfWork.SaveChanges();
                transaction.Complete();
            }

            using (var session = CreateSession())
            {
                var entities = session.QueryOver<Foo>().List();
                Assert.Equal(1, entities.Count);
            }
        }

        [Fact]
        public void ShouldHaveNestedTransactions()
        {
            using(var transaction1 = new TransactionRange(UnitOfWork))
            {
                using (var transaction2 = new TransactionRange(UnitOfWork))
                {
                    var entity1 = fixture.CreateAnonymous<Foo>();
                    fooRepository.Add(entity1);
                    UnitOfWork.SaveChanges();
                    transaction2.Complete();
                }

                var entity = fixture.CreateAnonymous<Foo>();
                fooRepository.Add(entity);
                UnitOfWork.SaveChanges();
                transaction1.Complete();
            }

            using (var session = CreateSession())
            {
                var entities = session.QueryOver<Foo>().List();
                Assert.Equal(2, entities.Count);
            }
        }

        [Fact]
        public void ShouldHaveDeepNestedTransactions()
        {
            using (var transaction1 = new TransactionRange(UnitOfWork))
            using (var transaction2 = new TransactionRange(UnitOfWork))
            {
                using (var transaction3 = new TransactionRange(UnitOfWork))
                using (var transaction4 = new TransactionRange(UnitOfWork))
                {
                    transaction4.Complete();
                    var entity1 = fixture.CreateAnonymous<Foo>();
                    fooRepository.Add(entity1);
                    UnitOfWork.SaveChanges();
                    transaction3.Complete();
                }
                transaction2.Complete();
                var entity = fixture.CreateAnonymous<Foo>();
                fooRepository.Add(entity);
                UnitOfWork.SaveChanges();
                transaction1.Complete();
            }

            using (var session = CreateSession())
            {
                var entities = session.QueryOver<Foo>().List();
                Assert.Equal(2, entities.Count);
            }
        }

        [Fact]
        public void ShouldRollbackWhenNestedTransactionDoNotComplete()
        {
            using(var transaction1 = new TransactionRange(UnitOfWork))
            {
                using (new TransactionRange(UnitOfWork))
                {
                    var entity1 =
                        fixture.
                            CreateAnonymous
                            <Foo>();
                    fooRepository.Add(
                        entity1);
                    UnitOfWork.SaveChanges();
                }

                var entity = fixture.CreateAnonymous<Foo>();
                fooRepository.Add(entity);
                UnitOfWork.SaveChanges();
                transaction1.Complete();
            }


            using (var session = CreateSession())
            {
                var entities = session.QueryOver<Foo>().List();
                Assert.Equal(0, entities.Count);
            }
        }

        public void Dispose()
        {
            TransactionRange.CreateTransactionRangeManager = null;
        }
    }
}