package archunit.modules.module2;

import archunit.modules.module1.A;

public class B {
    private A CreateA() {
        return new A(this);
    }
}
