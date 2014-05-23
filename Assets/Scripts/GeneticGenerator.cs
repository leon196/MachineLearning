using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GeneticGenerator : MonoBehaviour
{
    List<Plant> population;     // population à faire évoluer
    int populationSize;         // taille de la population
    int numberOfAttributes;     // nombre de chromosomes par individu
    int sequenceLength;         // longueur des chromosomes
    int nbOfTurnsPerCycle;      // nombre de tours par cycle
    float mutationProbability;  // probabilité de mutation 
    float elitism;              // probabilité que le vainqueur d"un affrontement soit le plus adapté des deux
 

    void Start()
    {
        populationSize = 20;
        numberOfAttributes = 4;
        sequenceLength = 10;
        mutationProbability = 0.05f;
        elitism = 0.75f;
        nbOfTurnsPerCycle = 30;
        population = generateRandomPopulation(populationSize,numberOfAttributes,sequenceLength);
    }

    void Run()
    {   
        // Debug.Log("!!!!!! NOUVELLE PARTIE !!!!!!"); 
        int nbOfTurn = 0;

        float avgFitness = 0f;  // coefficient d'adaptation moyen de la population
        float maxFitness = 0f;  // coefficient d'adaptation maximal
        Plant winner = null;    // individu ayant ce coefficient maximal

        while (nbOfTurn < nbOfTurnsPerCycle)
        {
            nbOfTurn += 1;
            maxFitness = 0;
            float sumFitness = 0;
            for (int i=0; i<populationSize; i++)
            {
                float newFitness = evaluateFitness(population[i]);         // on joue une partie pour évaluer le fitness de la plante
                sumFitness += newFitness;    
                if (newFitness > maxFitness)
                {
                    maxFitness = newFitness;
                    winner = population[i];
                }
            }
            avgFitness = sumFitness/(float)populationSize;
    
            /*Debug.Log("//" + nbOfTurn.ToString());
            Debug.Log("maximum fitness = " + maxFitness.ToString() + "   average fitness = " + avgFitness.ToString() );*/     
    
            // selection des parents par tournois succesifs parmi la population
            List<Plant> parents = selectParents(elitism);
            List<Plant> temporaryParents = new List<Plant>(parents);   // copie de la liste utilisée pour la condition d'arrêt de la boucle while qui suit
    
            List<Plant> children = new List<Plant>(populationSize);
            while (temporaryParents.Count>0)
            {
                int index1 = (int)Random.Range(0, temporaryParents.Count-1);
                int index2 = (int)Random.Range(0, temporaryParents.Count-1);
                while (index2==index1)
                {
                    index2 = (int)Random.Range(0, temporaryParents.Count-1);
                }
                int splitIndex = (int)Random.Range(0,sequenceLength-1);
                Plant child1 = crossIndividuals(temporaryParents[index1],temporaryParents[index2],splitIndex)[0];
                Plant child2 = crossIndividuals(temporaryParents[index1],temporaryParents[index2],splitIndex)[1];
                children.Add(child1);
                children.Add(child2);
                if (index1 > index2)
                {
                    int temp = index1;
                    index1 = index2;
                    index2 = temp;
                }
                temporaryParents.RemoveAt(index2);
                temporaryParents.RemoveAt(index1);
                //Debug.Log("Il reste " + str(len(parents)) + " parents")
            }

            // Seuls les enfants ont une probabilité de muter    
            mutatePopulation(children, mutationProbability);

            // On peut choisir le pourcentage de parents qui seront remplacés par leurs enfants.
            for (int i=0; i<children.Count; i++)
            {
                population.Add(children[i]);
            }
        }
        //Debug.Log("le gagnant est : \n" + str(winner));
    }

    List<Plant> generateRandomPopulation(int populationSize, int numberOfAttributes, int sequenceLength)
    {
        
        List<Plant> randomPopulation = new List<Plant>(populationSize);
        for (int i=0; i<populationSize; i++)
        {
            List<List<short>> attributes = new List<List<short>>(numberOfAttributes);
            for (int j=0; j<numberOfAttributes; j++)
            {
                List<short> newAttribute = new List<short>(sequenceLength);  
                for (int k=0; k<sequenceLength; k++)
                {
                    newAttribute[k] = (short)Random.Range(0,1);
                }
                attributes.Add(newAttribute);
            }
            /*Plant newIndividual = new Plant(attributes);
            randomPopulation.Add(newIndividual);*/
        }
        return randomPopulation;
    }

    
    // fonction d'évaluation d'un individu
    float evaluateFitness(Plant individual)
    {
        float fitness = 42.0f; // score de la plante dans la partie
        
        // implémenter le déroulement du jeu, qui renvoie le score final de la plante
    
        //individual.fitness = 0
        //for a in individual.attributes:
        //    attributeValue = 0
        //    reversedAttribute = reversed(a)
        //    index = 0
        //    for bit in reversedAttribute :
        //        if bit==1: attributeValue += pow(2,index)
        //        index+=1
        //    individual.fitness += attributeValue
    
        return fitness;
    }

    // sélection des parents (doit etre utilisée sur une population de taille paire)
    // requierant un parametre compris entre 0.5 et 1.0
    List<Plant> selectParents(float elitism)
    {
        int populationSize = population.Count;
        List<Plant> newParents = new List<Plant>(2);
        while (population.Count > populationSize/2)
        {
            int index1 = Random.Range(0, population.Count-1);
            int index2 = Random.Range(0, population.Count-1);
            while (index2==index1)
                index2 = Random.Range(0,population.Count-1);
            if(index1==index2)
                Debug.Log("Attention : meme parent choisi 2 fois !");
        
            /*if (population[index1].fitness < population[index2].fitness)
                population[index1],population[index2] = population[index2],population[index1];*/

            // on est donc maintenant sûr que l'individu le mieux adapté est à la position index
            float r = Random.Range(0.0f,1.0f);
            if (r < elitism)
            {
                newParents.Add(population[index1]);
                population.RemoveAt(index1);
            }
            else
            {
                newParents.Add(population[index2]);
                population.RemoveAt(index2);
            }
        }
        return newParents;
    }
        

    void mutatePopulation(List<Plant> _population, float mutationProbability)
    {
        for (int i=0; i<_population.Count; i++)
        {
            for (int j=0; j < _population[i].attributes.Count; j++)
            {
                float r = Random.Range(0.0f,1.0f);
                if (r < mutationProbability)
                {
                    //Debug.Log('Mutation :\n')
                    //Debug.Log('   ' + str(i))
                    int index = Random.Range(0,_population[i].attributes.Count-1);
                    if (_population[i].attributes[j][index] == 0)
                        _population[i].attributes[j][index] = 1;
                    else
                        _population[i].attributes[j][index] = 0;
                    //Debug.Log('   ' + str(i))
                }
            }
        }
    }

    // croisement entre 2 individus, avec un seul point de croisement, à la position splitIndex
    Plant[] crossIndividuals(Plant indiv1, Plant indiv2, int splitIndex)
    {
        Plant[] children = new Plant[2];

        List<short[]> child1Attributes = new List<short[]>(numberOfAttributes);
        List<short[]> child2Attributes = new List<short[]>(numberOfAttributes);
    
        for (int i=0; i<indiv1.attributes.Count; i++)
        {
            short[] child1NewAttribute = crossSequences(indiv1.attributes[i],indiv2.attributes[i],splitIndex);
            child1Attributes.Add(child1NewAttribute);

            short[] child2NewAttribute = crossSequences(indiv2.attributes[i],indiv1.attributes[i],splitIndex);
            child2Attributes.Add(child2NewAttribute);
        }
        /*Plant child1 = new Plant(child1Attributes);
        Plant child2 = new Plant(child2Attributes);*/
        return children;
    }
    
    
    short[] crossSequences(short[] seq1, short[] seq2, int splitIndex)
    { 
        short[] newSequence = new short[sequenceLength];
        for (int i=0; i<seq1.Length; i++)
        {
            if(i<splitIndex)
            {
                newSequence[i] = seq1[i];
            }
            else
            {
                newSequence[i] = seq2[i];
            }
        }
        return newSequence;
    }
    
}