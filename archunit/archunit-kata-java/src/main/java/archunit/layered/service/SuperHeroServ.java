package archunit.layered.service;

import archunit.layered.infrastructure.SuperHeroRepository;

public class SuperHeroServ {
    private final SuperHeroRepository superHeroRepository;

    public SuperHeroServ(SuperHeroRepository superHeroRepository) {
        this.superHeroRepository = superHeroRepository;
    }

    public void getSuperHeroRepository() {

    }
}
