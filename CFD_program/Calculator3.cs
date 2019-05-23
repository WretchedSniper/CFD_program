using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFD_program
{
    class Calculator3
    {
    //    k(1,1)=h* s2(j);
    //    k(2,1)=h* s3(j);
    //    k(3,1)=(-0.5)*h* s1(j)*s3(j);
    //    k(1,2)=h* (s2(j)+k(2,1)/2);
    //k(2,2)=h* (s3(j)+k(3,1)/2);
    //k(3,2)=(-0.5)*h* (s1(j)+k(1,1)/2)*(s3(j)+k(3,1)/2);    
    //k(1,3)=h* (s2(j)+k(2,2)/2);
    //k(2,3)=h* (s3(j)+k(3,2)/2);
    //k(3,3)=(-0.5)*h* (s1(j)+k(1,2)/2)*(s3(j)+k(3,2)/2);    
    //k(1,4)=h* (s2(j)+k(2,3));
    //k(2,4)=h* (s3(j)+k(3,3));
    //k(3,4)=(-0.5)*h* (s1(j)+k(1,3))*(s3(j)+k(3,3));
    //%以上计算K矩阵
    //s1(j+1)=s1(j)+1/6*(k(1,1)+2*k(1,2)+2*k(1,3)+k(1,4));
    //s2(j+1)=s2(j)+1/6*(k(2,1)+2*k(2,2)+2*k(2,3)+k(2,4));
    //s3(j+1)=s3(j)+1/6*(k(3,1)+2*k(3,2)+2*k(3,3)+k(3,4));
    }
}
