//using infrastructure.interfaces;
//using infrastructure.models;
//using infrastructure.services;
//using moq;

//namespace infrastructure.tests.managers;

//public class productmanager_tests
//{
//    /* productmanager.SaveProduct(productrequest productrequest) = productservice.AddProductToList(productrequest) +
//       _fileService.saveobjectasjson(productlistfrommemory) - productrequest valid? list saved to file?
//    */
//    [fact]
//    public void saveproduct_shouldsucceed_whenproductisvalidandfilesaveworks()
//    {
//        // arrange
//        iproductservice productservice = new productservice();
//        mock<ifilerepository> filerepositorymock = new mock<ifilerepository>();
//        ifilerepository fakefilerepository = filerepositorymock.object; // låtsas-objektet som används i testet
//        // konfigurering, hur ska det simulerade objektet bete sig
//        filerepositorymock
//            .setup(filerepositorymock => filerepositorymock.saveobjectasjson(it.isany<ienumerable<productmodel>>()))
//            .returns(true); // hur ska 



//        // act


//        // assert
//    }

//}

////varför är det viktigt med interfaces här