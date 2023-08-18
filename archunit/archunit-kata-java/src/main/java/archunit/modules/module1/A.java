package archunit.modules.module1;

import archunit.modules.module2.B;

public class A {
    private final B b;

    public A(B b) {
        this.b = b;
    }
}
