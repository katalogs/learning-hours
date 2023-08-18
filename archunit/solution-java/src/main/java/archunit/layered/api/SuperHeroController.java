package archunit.layered.api;

import archunit.layered.domain.SuperHeroRepository;
import archunit.layered.service.SuperHeroService;

public class SuperHeroController {
    private final SuperHeroService superHeroService;
    private final SuperHeroRepository superHeroRepository;

    public SuperHeroController(SuperHeroService superHeroService, SuperHeroRepository superHeroRepository) {
        this.superHeroService = superHeroService;
        this.superHeroRepository = superHeroRepository;
    }
}
