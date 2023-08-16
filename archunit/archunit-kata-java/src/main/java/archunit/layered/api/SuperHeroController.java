package archunit.layered.api;

import archunit.layered.infrastructure.SuperHeroRepository;
import archunit.layered.service.SuperHeroServ;

public class SuperHeroController {
    private final SuperHeroServ superHeroService;
    private final SuperHeroRepository superHeroRepository;

    public SuperHeroController(SuperHeroServ superHeroService, SuperHeroRepository superHeroRepository) {
        this.superHeroService = superHeroService;
        this.superHeroRepository = superHeroRepository;
    }
}
