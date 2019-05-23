using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFD_program
{
    class Calculator2
    {
        public double H { set; get; }
        public double U { set; get; }
        public double L { set; get; }
        





    }
}

//%parameters
//h = 0.1;
//U = 1;
//L = 1;
//m = L/h + 1;
//A = zeros(m, m);
//B = zeros(m, m);

//%solve
//convergent1 = 0;
//k = 0;
//while(convergent1==0)
//    lastA = A;
//    k = k+1;
//    for i = 2:1:m-1
//        B(i,1) = (-A(i-1,2) + 8* A(i,2)/3 - A(i+1,2) - 2* A(i,3)/3) / h^2 + 2* U/(3* h);
//        B(i, m) = 0;
//        B(1, i) = (-A(2, i-1) + 8* A(2, i)/3 - A(2, i+1) - 2* A(3, i)/3) / h^2;
//        B(m, i) = (-A(m-1, i-1) + 8* A(m-1, i)/3 - A(m-1, i+1) - 2* A(m-2, i)/3) / h^2;        
//    end
//    B(1,1) = (B(1,2) + B(2,1))/2;
//    B(1, m) = (B(1,m-1) + B(2, m))/2;
//    B(m,1) = (B(m,2) + B(m-1,1))/2;
//    B(m, m) = (B(m-1,m) + B(m, m-1))/2;
    
//    convergent2 = 0;
//    while(convergent2==0)
//        lastB = B;
//        for i = 2:1:m-1
//            for j = 2:1:m-1
//                B(i, j) = (B(i-1,j) + B(i+1, j) + B(i, j-1) + B(i, j+1))/4;
//           end
//        end
//        if norm(B-lastB,2)<0.0001
//            convergent2 = 1;
//        end
//    end


//    convergent3 = 0;
//    while(convergent3==0)
//        innerlastA = A;
//        for i = 2:1:m-1
//            for j = 2:1:m-1
//                A(i, j) = (B(i, j) * h^2 + A(i-1, j) + A(i+1, j) + A(i, j-1) + A(i, j+1))/4;
//            end
//        end
//        if norm(A-innerlastA,2)<0.0001
//            convergent3 = 1;
//        end
//    end
    
//    if norm(A-lastA,2)<0.0001
//        convergent1 = 1;
//    end
//end